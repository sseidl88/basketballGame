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
     public class Ball_model
    {
        public int speed;
        public string color;
        public BoundingRectangle position;
        public bool isVisible = true;
        public Rectangle RectBounds
        {
            get { return position; }
        }
    }
}
