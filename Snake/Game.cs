using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Game
    {
        private const int Width = 30;
        private const int Height = 15;
        private const int refreshRate = 100;

        private Random gen = new Random();
        private Snake snake;
        private Tile[,] grid;
        private int points;
        private bool gameOver = false;
        private bool won = false;

        public Game()
        {
            points = 0;
            grid = new Tile[Height, Width];
            snake = new Snake(new Position(Width / 2, Height / 2));
            InitGrid();
            AddFruit();
        }

        private void InitGrid()
        {
            foreach (Position part in snake.Tail)
            {
                grid[part.Y, part.X] = Tile.Snake;
            }

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (j == 0 || j == Width - 1 || i == 0 || i == Height - 1)
                        grid[i, j] = Tile.Wall;
                }
            }
        }

        private void AddFruit()
        {
            var emptyPositions = GetEmptyPositions();

            if (emptyPositions.Count == 0)
            {
                won = true;
                return;
            }

            var newFruitPos = emptyPositions[gen.Next(emptyPositions.Count)];
            grid[newFruitPos.Y, newFruitPos.X] = Tile.Fruit;
        }

        private List<Position> GetEmptyPositions()
        {
            var result = new List<Position>();

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (grid[i, j] == Tile.Empty)
                        result.Add(new Position(j, i));
                }
            }

            return result;
        }

        private Position GetNextSnakePos()
        {
            var oldPos = snake.Tail.First.Value;
            if (snake.Direction == Direction.North)
                return new Position(oldPos.X, oldPos.Y - 1);
            else if (snake.Direction == Direction.South)
                return new Position(oldPos.X, oldPos.Y + 1);
            else if (snake.Direction == Direction.East)
                return new Position(oldPos.X + 1, oldPos.Y);
            else if (snake.Direction == Direction.West)
                return new Position(oldPos.X - 1, oldPos.Y);

            return oldPos;
        }

        private void Move()
        {
            var nextPos = GetNextSnakePos();
            var tile = grid[nextPos.Y, nextPos.X];

            if (tile == Tile.Empty || nextPos == snake.TailPosition())
            {
                grid[snake.TailPosition().Y, snake.TailPosition().X] = Tile.Empty;
                snake.RemoveTail();
                grid[nextPos.Y, nextPos.X] = Tile.Snake;
                snake.AddHead(nextPos);
            }
            else if (tile == Tile.Fruit)
            {
                grid[nextPos.Y, nextPos.X] = Tile.Snake;
                snake.AddHead(nextPos);
                AddFruit();
                points++;
            }
            else if (tile == Tile.Wall || tile == Tile.Snake)
            {
                gameOver = true;
            }
        }

        private void ChangeDirection()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.W || key == ConsoleKey.UpArrow)
                    snake.ChangeDirection(Direction.North);
                else if (key == ConsoleKey.S || key == ConsoleKey.DownArrow)
                    snake.ChangeDirection(Direction.South);
                else if (key == ConsoleKey.A || key == ConsoleKey.LeftArrow)
                    snake.ChangeDirection(Direction.West);
                else if (key == ConsoleKey.D || key == ConsoleKey.RightArrow)
                    snake.ChangeDirection(Direction.East);
            }
        }

        public void Run()
        {
            while (!gameOver && !won)
            {
                Draw();
                ChangeDirection();
                Move();
                Thread.Sleep(refreshRate);
            }
        }

        private void Draw()
        {
            Console.Clear();

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (grid[i, j] == Tile.Empty)
                        Console.Write(" ");
                    else if (grid[i, j] == Tile.Snake)
                        Console.Write("*");
                    else if (grid[i, j] == Tile.Fruit)
                        Console.Write("o");
                    else if (grid[i, j] == Tile.Wall)
                        Console.Write("#");
                }

                Console.Write("\n");
            }
        }
    }
}
