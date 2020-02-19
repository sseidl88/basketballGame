﻿using System;
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
    public class EnemyModel
    {
        public int speed = 4;
        public string color;
        public BoundingRectangle position;
        public bool isVisible = true;
        public Texture2D texture;
        public Rectangle RectBounds
        {
            get { return position; }
        }
        public void Update()
        {
            position.X += speed;

            if (position.X < 80)
            {
                position.X = 80;
                speed *= -1;
            }

            if (position.X > 620)
            {
                position.X = 620;
                speed *= -1;
            }

        }
    }
}