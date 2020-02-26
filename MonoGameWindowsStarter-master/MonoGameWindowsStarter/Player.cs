using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace MonoGameWindowsStarter
{
    enum GameStatus
    {
        playing,
        gameOver
    }
    //enum PlayerAnimation
    //{
    //    Shooting = 1,
    //    Idle = 2,
    //    dribbleUp = 3,
    //    dribbleDown = 4,
    //}
    enum PlayerAnimation
    {
        Shooting = 3,
        Idle = 2,
        dribbleUp = 1,
        dribbleDown = 4,
    }
    public class Player
    {
        Game1 game;
        ContentManager content;
        public BoundingRectangle bounds;
        public Rectangle RectBounds
        {
            get { return bounds; }
        }
        Texture2D texture;
        public int speed;
        GraphicsDevice graphics;
        public List<Basketball> ballList = new List<Basketball>();
        public List<Ball_model> shots = new List<Ball_model>();
        public List<Moneyball> money = new List<Moneyball>();
        private KeyboardState oldKeyboardState;
        Hoop hoop1;
        Hoop hoop2;
        Hoop hoop3;
        int score = 0;
        SpriteFont font;
        Vector2 scoreboardPosition = new Vector2( 500, 150);
        Vector2 timerPosition = new Vector2(200, 150);
        Random random;
        int ballChoice;
        float timer = 60;
        bool playing = true;
        EnemyType1 enemy;
        EnemyType1 enemy2;
        EnemyType1 enemy3;
        EnemyType1 enemy4;
        EnemyType1 enemy5;

        const int ANIMATION_FRAME_RATE = 124;
        TimeSpan timerRate;
        PlayerAnimation animateState;

        int frame;
        const int FRAME_WIDTH = 64;

        const int FRAME_HEIGHT = 64;

       // const float PLAYER_SPEED = 150;


        GameStatus GameStatus = GameStatus.playing;

        public Player(Game1 game)
        {
            this.game = game;
            timerRate = new TimeSpan(0);
            hoop1 = new Hoop(100, -550);
            hoop2 = new Hoop(350, -520);
            hoop3 = new Hoop(600, -550);
            enemy = new EnemyType1(90, 200);
            enemy2 = new EnemyType1(550, 300);
            enemy3 = new EnemyType1(250, 0);
            enemy4 = new EnemyType1(350, -200);
            enemy5 = new EnemyType1(450, -350);
            //recBounds = bounds;
            bounds.X = 300;
            bounds.Height = 10;
            bounds.Width = 10;


        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            texture = content.Load<Texture2D>("ballSheet");
            speed = 2;
            random = new Random();
            font = content.Load<SpriteFont>("font");

            //set bounds size and placement
           
            bounds.X = 400;
            bounds.Y = 400;
            enemy.LoadContent(content);
            enemy2.LoadContent(content);
            enemy3.LoadContent(content);
            enemy4.LoadContent(content);
            enemy5.LoadContent(content);

        }

        public void Update(GameTime gametime)
        {
            var keyboardState = Keyboard.GetState();

            ballChoice = random.Next(1, 10);
            enemy.Update();
            enemy2.Update();
            enemy3.Update();
            enemy4.Update();
            enemy5.Update();
            if (!playing)
            {
                GameStatus = GameStatus.gameOver;
                enemy.isMoving = false;
                enemy2.isMoving = false;
                enemy3.isMoving = false;
                enemy4.isMoving = false;
                enemy5.isMoving = false;

            }


            if (playing)
            timer -= (float)gametime.ElapsedGameTime.TotalSeconds;
            if(timer <= 0)
            {
                timer = 0;
                playing = false;
            }
            if(keyboardState.IsKeyDown(Keys.Enter) && !playing)
            {
                playing = true;
                GameStatus = GameStatus.playing;
                timer = 60;
                score = 0;
                bounds.X = 300;
                bounds.Y = 450;
                enemy.isMoving = true;
                enemy2.isMoving = true;
                enemy3.isMoving = true;
                enemy4.isMoving = true;
                enemy5.isMoving = true;
            }

            if (keyboardState.IsKeyDown(Keys.Right) && playing)
            {
                animateState = PlayerAnimation.dribbleUp;
                if(bounds.X > 580)
                {
                    bounds.X = 580;
                }
                bounds.X += speed;
                animateState = PlayerAnimation.dribbleDown;
            }
            else if (keyboardState.IsKeyDown(Keys.Left) && playing)
            {
                animateState = PlayerAnimation.dribbleUp;
                if (bounds.X < 60)
                {
                    bounds.X = 60;
                }
                bounds.X -= speed;
                animateState = PlayerAnimation.dribbleDown;
            }
            else if (keyboardState.IsKeyDown(Keys.Up) && playing)
            {
                animateState = PlayerAnimation.dribbleUp;
                if (bounds.Y < -450)
                {
                    bounds.Y = -450;
                }
                //keep the score and time in the play's field of view
                scoreboardPosition.Y -= speed;
                timerPosition.Y -= speed;

                bounds.Y -= speed;
                animateState = PlayerAnimation.dribbleDown;
            }
            else if (keyboardState.IsKeyDown(Keys.Down) && playing)
            {
                animateState = PlayerAnimation.dribbleUp;
                if (bounds.Y > 400)
                {
                    bounds.Y = 400;
                }   
                //keep the score and time in the play's field of view
                scoreboardPosition.Y += speed;
                timerPosition.Y += speed;

                bounds.Y += speed;
                animateState = PlayerAnimation.dribbleDown;
            }else
            {
                animateState = PlayerAnimation.Idle;
            }


            if(scoreboardPosition.Y < -550 || timerPosition.Y < -550)
            {
                scoreboardPosition.Y = -550;
                timerPosition.Y = -550;
            }
            if (scoreboardPosition.Y > 150 || timerPosition.Y > 150)
            {
                scoreboardPosition.Y = 150;
                timerPosition.Y = 150;
            }


            //shoot
            if (keyboardState.IsKeyDown(Keys.Space) && !oldKeyboardState.IsKeyDown(Keys.Space) && playing)
            {
                animateState = PlayerAnimation.Shooting;
                if(ballChoice >= 9)
                {
                    money.Add(new Moneyball(this));
                }else
                    shots.Add(new Basketball(this));
            }
            oldKeyboardState = keyboardState;

            foreach (Basketball bb in shots)
            {
                bb.Update(gametime);
            }
            foreach(Moneyball mb in money)
            {
                mb.Update(gametime);
            }
            //remove basketball after no longer visible
            for (int i = 0; i < shots.Count; i++)
            {
                if (!shots[i].isVisible)
                {
                    shots.RemoveAt(i);
                    i--;
                }
            }
            //remove moneyball after no longer visible
            for (int i = 0; i < money.Count; i++)
            {
                if (!money[i].isVisible)
                {
                    money.RemoveAt(i);
                    i--;
                }
            }

            //check collision with hoop for bb
            foreach (Basketball bb in shots)
            {
                if (bb.RectBounds.Intersects(hoop1.RectBounds) || bb.RectBounds.Intersects(hoop2.RectBounds) || bb.RectBounds.Intersects(hoop3.RectBounds))
                {
                    bb.isVisible = false;
                    score += 3;
                }
                //if touching enemies disapear
                if (bb.RectBounds.Intersects(enemy.RectBounds) || bb.RectBounds.Intersects(enemy2.RectBounds))
                {
                    bb.isVisible = false;
                }

            }
            //check collision with hoop for moneyball
            foreach (Moneyball mb in money)
            {
                if (mb.RectBounds.Intersects(hoop1.RectBounds) || mb.RectBounds.Intersects(hoop2.RectBounds) || mb.RectBounds.Intersects(hoop3.RectBounds))
                {
                    mb.isVisible = false;
                    score += 10;
                }
                if (mb.RectBounds.Intersects(enemy.RectBounds)|| mb.RectBounds.Intersects(enemy2.RectBounds))
                {
                    mb.isVisible = false;
                }

            }

            //check collision of enemies and player
            if (RectBounds.Intersects(enemy.RectBounds) || RectBounds.Intersects(enemy2.RectBounds)|| RectBounds.Intersects(enemy3.RectBounds) || RectBounds.Intersects(enemy4.RectBounds) || RectBounds.Intersects(enemy5.RectBounds))
            {
                playing = false;
            }

            //animation
            if (animateState != PlayerAnimation.Idle) timerRate += gametime.ElapsedGameTime;


            while (timerRate.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {

                frame++;

                timerRate -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }

            frame %= 1;




        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var source = new Rectangle(
            frame * FRAME_WIDTH, // X value 
            (int)animateState % 4 * FRAME_HEIGHT, // Y value
            FRAME_WIDTH, // Width 
            FRAME_HEIGHT // Height
            );

            spriteBatch.Draw(texture, new Vector2(bounds.X, bounds.Y), source, Color.White);
            spriteBatch.DrawString(font, "Score: " + score.ToString(), scoreboardPosition, Color.White);
            spriteBatch.DrawString(font, "Time Left: " + timer.ToString("0.0"), timerPosition, Color.White);
            if (!playing)
            {
                spriteBatch.DrawString(font, "GAME OVER", new Vector2(300, bounds.Y - 150), Color.White);
                spriteBatch.DrawString(font, "Press Enter to Restart", new Vector2(300, bounds.Y - 50), Color.White);
            }
            

            enemy.Draw(spriteBatch);
            enemy2.Draw(spriteBatch);
            enemy3.Draw(spriteBatch);
            enemy4.Draw(spriteBatch);
            enemy5.Draw(spriteBatch);

            foreach (Basketball bb in shots)
            {
                bb.LoadContent(content);
                bb.Draw(spriteBatch);
            }
            foreach(Moneyball mb in money)
            {
                mb.LoadContent(content);
                mb.Draw(spriteBatch);
            }
        }


    }
}
