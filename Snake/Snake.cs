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
        public LinkedList<Position> Tail { get; }
        public Direction Direction { get; private set; }

        public Snake(Position pos)
        {
            Tail = new LinkedList<Position>();
            Direction = Direction.East;

            for (int i = 2; i >= 0; i--)
            {
                Tail.AddLast(new Position(pos.X + i, pos.Y));
            }
        }

        public Position HeadPosition()
        {
            return Tail.First();
        }

        public Position TailPosition()
        {
            return Tail.Last();
        }

        public void AddHead(Position pos)
        {
            Tail.AddFirst(pos);
        }

        public void RemoveTail()
        {
            Tail.RemoveLast();
        }

        public void ChangeDirection(Direction dir)
        {
            if ((dir == Direction.North && Direction != Direction.South) ||
                (dir == Direction.South && Direction != Direction.North) ||
                (dir == Direction.West && Direction != Direction.East) ||
                (dir == Direction.East && Direction != Direction.West))
                Direction = dir;
        }
    }
}
