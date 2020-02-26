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
    public class Player
    {
        Game1 game;
        ContentManager content;
        public BoundingRectangle bounds;
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
        Vector2 scoreboardPosition = new Vector2( 500, 20);
        Vector2 timerPosition = new Vector2(200, 20);
        Random random;
        int ballChoice;
        float timer = 60;
        bool playing = true;
        EnemyType1 enemy;
        EnemyType1 enemy2;

        public Player(Game1 game)
        {
            this.game = game;
            hoop1 = new Hoop(100, -550);
            hoop2 = new Hoop(350, -520);
            hoop3 = new Hoop(600, -550);
            enemy = new EnemyType1(90, 200);
            enemy2 = new EnemyType1(550, 300);

        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            texture = content.Load<Texture2D>("bbal_player");
            speed = 4;
            random = new Random();
            font = content.Load<SpriteFont>("font");

            //set bounds size and placement
            bounds.Width = 80;
            bounds.Height = 100;
            bounds.X = 400;
            bounds.Y = 400;
            enemy.LoadContent(content);
            enemy2.LoadContent(content);

        }

        public void Update(GameTime gametime)
        {
            var keyboardState = Keyboard.GetState();

            ballChoice = random.Next(1, 10);
            enemy.Update();
            enemy2.Update();
            if (!playing)
            {
                enemy.isMoving = false;
                enemy2.isMoving = false;
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
                timer = 60;
                score = 0;
            }

            if (keyboardState.IsKeyDown(Keys.Right) && playing)
            {
                if(bounds.X > 660)
                {
                    bounds.X = 660;
                }
                bounds.X += speed;
            }
            if (keyboardState.IsKeyDown(Keys.Left) && playing)
            {
                if(bounds.X < 0)
                {
                    bounds.X = 0;
                }
                bounds.X -= speed;
            }
            //adding the ability to move up and down the screen
            if (keyboardState.IsKeyDown(Keys.Up) && playing)
            {
                if(bounds.Y < -450)
                {
                    bounds.Y = -450;
                }
                scoreboardPosition.Y -= speed;
                timerPosition.Y -= speed;
                bounds.Y -= speed;
            }
            if (keyboardState.IsKeyDown(Keys.Down) && playing)
            {
                if(bounds.Y > 400)
                {
                    bounds.Y = 400;
                }   
                bounds.Y += speed;
            }
            //shoot
            if (keyboardState.IsKeyDown(Keys.Space) && !oldKeyboardState.IsKeyDown(Keys.Space) && playing)
            {
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


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(bounds.X, bounds.Y));
            spriteBatch.DrawString(font, "Score: " + score.ToString(), scoreboardPosition, Color.White);
            spriteBatch.DrawString(font, "Time Left: " + timer.ToString("0.0"), timerPosition, Color.White);
            if (!playing)
            {
                spriteBatch.DrawString(font, "GAME OVER", new Vector2(300, 200), Color.White);
                spriteBatch.DrawString(font, "Press Enter to Restart", new Vector2(300, 270), Color.White);
            }
            

            enemy.Draw(spriteBatch);
            enemy2.Draw(spriteBatch);

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
