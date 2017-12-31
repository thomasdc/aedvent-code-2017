using System;
using System.Collections.Generic;
using System.IO;

namespace Tubes
{
    class Program
    {
        static void Main(string[] args)
        {
            var (grid, startingLocation) = Parse(File.ReadAllLines("input.txt"));
            Console.WriteLine(Part1(grid, startingLocation));
            Console.ReadLine();
        }

        private static string Part1(char[,] grid, (int x, int y) startingLocation)
        {
            var packet = new Packet(startingLocation);
            var moving = true;
            while (moving)
            {
                moving = packet.Move(grid);
            }

            return string.Join("", packet.CollectedLetters);
        }

        private static (char[,] grid, (int x, int y) startingLocation) Parse(string[] input)
        {
            var grid = new char[input[0].Length, input.Length];
            (int x, int y) startingLocation = (0, 0);

            for (var y = 0; y < input.Length; y++)
            {
                var line = input[y];
                for (var x = 0; x < line.Length; x++)
                {
                    grid[x, y] = line[x];
                    if (y == 0 && line[x] == '|')
                    {
                        startingLocation = (x, y);
                    }
                }
            }
            
            return (grid, startingLocation);
        }

        class Packet
        {
            private (int x, int y) _currentLocation;
            private Direction _direction;
            public readonly IList<char> CollectedLetters = new List<char>();

            public Packet((int x, int y) startingLocation)
            {
                _currentLocation = startingLocation;
                _direction = Direction.Down;
            }

            public bool Move(char[,] grid)
            {
                var @char = grid[_currentLocation.x, _currentLocation.y];
                if (@char == '+') // switching direction!
                {
                    if (_direction == Direction.Left || _direction == Direction.Right)
                    {
                        if (grid[_currentLocation.x, _currentLocation.y - 1] != ' ')
                        {
                            _direction = Direction.Up;
                        } 
                        else if (grid[_currentLocation.x, _currentLocation.y + 1] != ' ')
                        {
                            _direction = Direction.Down;
                        }
                    }
                    else if (_direction == Direction.Up || _direction == Direction.Down)
                    {
                        if (grid[_currentLocation.x + 1, _currentLocation.y] != ' ')
                        {
                            _direction = Direction.Right;
                        }
                        else if (grid[_currentLocation.x - 1, _currentLocation.y] != ' ')
                        {
                            _direction = Direction.Left;
                        }
                    }
                }
                else if (@char == '-' || @char == '|')
                {
                    // Move along https://media2.giphy.com/media/13d2jHlSlxklVe/giphy.gif
                }
                else if (@char == ' ')
                {
                    // End of maze
                    return false;
                }
                else
                {
                    CollectedLetters.Add(@char);
                }

                MoveInDirection(_direction);
                return true;
            }

            private void MoveInDirection(Direction direction)
            {
                switch (direction)
                {
                    case Direction.Up: _currentLocation = (_currentLocation.x, _currentLocation.y - 1); break;
                    case Direction.Right: _currentLocation = (_currentLocation.x + 1, _currentLocation.y); break;
                    case Direction.Down: _currentLocation = (_currentLocation.x, _currentLocation.y + 1); break;
                    case Direction.Left: _currentLocation = (_currentLocation.x - 1, _currentLocation.y); break;
                }
            }
        }

        enum Direction
        {
            Right, Left, Up, Down
        }
    }
}
