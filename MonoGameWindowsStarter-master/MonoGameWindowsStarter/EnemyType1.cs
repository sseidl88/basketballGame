using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter
{
    public class EnemyType1: EnemyModel
    {
        ContentManager content;
        public EnemyType1(int x, int y)
        {
            position.Width = 5;
            position.Height = 5;
            position.X = x;
            position.Y = y;
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            texture = this.content.Load<Texture2D>("bball_enemy");
        }
        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), Color.White);
        }
    }
}
