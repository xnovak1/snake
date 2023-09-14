using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Snake
    {
        // Snake head is first in list
        private List<SnakePart> parts;
        private Direction direction;
        
        public enum Direction
        {
            North,
            East,
            South,
            West
        }

        public Snake()
        {
            parts = new List<SnakePart>();
            AddPart();
            direction = Direction.East;
        }

        public void AddPart()
        {
            if (parts.Count > 0)
                parts.Add(new SnakePart(5, 5));

            var lastPart = parts.Last();
            int newX = lastPart.X;
            int newY = lastPart.Y;

            if (direction == Direction.North)
                newY--;
            else if (direction == Direction.South)
                newY++;
            else if (direction == Direction.West)
                newX++;
            else if (direction == Direction.East)
                newX--;

            parts.Add(new SnakePart(newX, newY));
        }

        public void ChangeDirection(Direction dir)
        {
            direction = dir;
        }

        public void Move()
        {
            foreach (SnakePart part in parts)
            {
                if (direction == Direction.North)
                    part.Y--;
                else if (direction == Direction.South)
                    part.Y++;
                else if (direction == Direction.East)
                    part.X++;
                else if (direction == Direction.West)
                    part.X--;
            }
        }
    }
}
