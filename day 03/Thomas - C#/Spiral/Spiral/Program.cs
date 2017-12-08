using System;
using System.Collections.Generic;
using System.Linq;

namespace Spiral
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part2(265149));
        }

        public static int Part1(int input)
        {
            return Cells().Skip(input-1).Select(_ => Math.Abs(_.column) + Math.Abs(_.row)).First();
        }

        public static int Part2(int input)
        {
            var grid = new Grid();
            grid.Set(0, 0, 1);
            foreach (var cell in Cells().Skip(1))
            {
                var sum = GetSumOfAdjacentCells(grid, cell);
                grid.Set(cell.column, cell.row, sum);
                if (sum > input)
                {
                    return sum;
                }
            }
            
            return -1;
        }

        private static int GetSumOfAdjacentCells(Grid grid, (int row, int column) cell)
        {
            var sum = 0;
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    sum += grid.Get(cell.column + i, cell.row + j);
                }
            }
            return sum;
        }

        private static IEnumerable<(int row, int column)> Cells()
        {
            var column = 0;
            var row = 0;
            var depth = 0;
            
            yield return (row, column);
            
            while (true)
            {
                if (row == -depth) // bottom row
                {
                    if (column == depth) // right corner
                    {
                        depth++;
                    }
                    column++;
                }
                else if (row == depth) // top row
                {
                    if (column == -depth) // left corner
                    {
                        row--;
                    }
                    else
                    {
                        column--;
                    }
                }
                else if (column == depth) // right middle
                {
                    row++;
                }
                else // left middle
                {
                    row--;
                }

                yield return (row, column);
            }
        }

        private class Grid
        {
            private const int dimension = 1000;
            private const int offset = dimension / 2;
            private readonly int[,] _grid = new int[dimension, dimension];

            public void Set(int column, int row, int value)
            {
                _grid[row + offset, column + offset] = value;
            }

            public int Get(int column, int row)
            {
                return _grid[row + offset, column + offset];
            }
        }
    }
}
