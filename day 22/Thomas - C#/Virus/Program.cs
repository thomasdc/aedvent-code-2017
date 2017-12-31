using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Virus
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part2(Parse(File.ReadAllLines("input.txt"))));
            Console.ReadLine();
        }

        private static int Part2(IDictionary<(int, int), State> grid)
        {
            var carrier = new Carrier();
            foreach (var iteration in Enumerable.Range(0, 10000000))
            {
                carrier.Burst(grid);
            }

            return carrier.NumberOfInfections;
        }

        private static IDictionary<(int, int), State> Parse(string[] inputLines)
        {
            var gridSize = inputLines.First().Length;
            var grid = new Dictionary<(int, int), State>();

            for (var y = gridSize / 2; y >= -gridSize / 2; y--)
            {
                var line = inputLines[-(y - gridSize / 2)];
                for (var x = -gridSize / 2; x <= gridSize / 2; x++)
                {
                    var @char = line[x + gridSize / 2];
                    grid[(x, y)] = @char == '#' ? State.Infected : State.Clean;
                }
            }

            return grid;
        }

        class Carrier
        {
            private Direction _direction = Direction.Up;
            private (int x, int y) _location = (0, 0);
            public int NumberOfInfections;
            
            public void Burst(IDictionary<(int, int), State> grid)
            {
                if (!grid.ContainsKey(_location))
                {
                    grid[_location] = State.Clean;
                }

                switch (grid[_location])
                {
                    case State.Clean: Turn(true); break;
                    case State.Infected: Turn(false); break;
                    case State.Flagged: Reverse(); break;
                }
                
                if (grid[_location] == State.Weakened)
                {
                    NumberOfInfections++;
                }

                grid[_location] = (State) (((int) grid[_location] + 1) % 4);
                MoveForward();
            }

            private void MoveForward()
            {
                switch (_direction)
                {
                    case Direction.Up: _location = (_location.x, _location.y + 1); break;
                    case Direction.Right: _location = (_location.x + 1, _location.y); break;
                    case Direction.Down: _location = (_location.x, _location.y - 1); break;
                    case Direction.Left: _location = (_location.x - 1, _location.y ); break;
                }
            }

            private void Turn(bool left)
            {
                _direction = left
                    ? (Direction) (((int) _direction - 1 + 4) % 4)
                    : (Direction) (((int) _direction + 1) % 4);
            }

            private void Reverse()
            {
                _direction = (Direction) (((int) _direction + 2) % 4);
            }
        }

        enum Direction
        {
            Up = 0, Right = 1, Down = 2, Left = 3
        }

        enum State
        {
            Clean = 0, Weakened = 1, Infected = 2, Flagged = 3
        }
    }
}
