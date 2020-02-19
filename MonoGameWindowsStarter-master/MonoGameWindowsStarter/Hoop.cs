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
    public class Hoop
    {
        Game1 game;
        ContentManager content;
        BoundingRectangle position;
        Texture2D texture;

        public Rectangle RectBounds
        {
            get { return position; }
        }

        public Hoop(int x, int y)
        {
            position.X = x;
            position.Y = y;
            position.Height = 50;
            position.Width = 50;
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            texture = content.Load<Texture2D>("hoop");
        }

        public void Update()
        {

        }


        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, new Vector2(position.X, position.Y), Color.White);
        }

    }
}
