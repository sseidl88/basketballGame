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
    public class Moneyball: Ball_model
    {
        ContentManager content;
        public Texture2D ball;
        public Moneyball(Player player)
        {
            position.X = player.bounds.X + 10;
            position.Y = player.bounds.Y - 20;
            speed = 3;
            position.Width = 10;
            position.Height = 10;
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            ball = this.content.Load<Texture2D>("moneyball");
        }

        public void Update(GameTime gameTime)
        {
            if (position.Y < -550)
            {
                isVisible = false;
            }

            position.Y -= speed;


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ball, new Vector2(position.X, position.Y), Color.White);
        }
    }
}
