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
    public class Grid
    {
        Game1 game;
        int NUM_CELLS = 4;
        int CELL_SIZE_x = 250;
        int CELL_SIZE_y = 250;
        public Grid(Game1 game)
        {
            this.game = game;

            //clear the grid
            for(int x = 0; x < NUM_CELLS; x++)
            {
                for(int y = 0; y < NUM_CELLS; y++)
                {

                }
            }

        }
    }
}
