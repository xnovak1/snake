using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Game
    {
        private Map map;
        private Snake snake;
        private int points;
        private const int refreshRate = 100;
        private bool finished = false;

        public Game()
        {
            points = 0;
            map = new Map();
            snake = new Snake();
        }

        public void Run()
        {
            while (true)
            {
                Update();

                if (finished)
                {
                    DrawFinished();
                    return;
                }

                Draw();
            }
        }

        private bool Update()
        {
            // move snake
            // collisions? (check only head)
            //      YES -> Wall  -> GAME OVER
            //             Snake -> GAME OVER
            //             Food  -> add part, delete food, spawn new food
            //      NO  -> do nothing

            return true;
        }

        private void Draw()
        {
            // grid
            // points
        }

        private void DrawFinished()
        {
            Draw();
            // draw game over message
        }
    }
}
