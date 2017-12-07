using System;

namespace Spiral
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part1(265149));
        }

        public static int Part1(int input)
        {
            var column = 0;
            var row = 0;
            var depth = 0;
            for (var counter = 1; counter < input; counter++)
            {
                (depth, row, column) = Next(depth, row, column);
            }
            
            return Math.Abs(column) + Math.Abs(row);
        }

        public static (int nextDepth, int nextRow, int nextColumn) Next(int depth, int row, int column)
        {
            var nextDepth = depth;
            var nextRow = row;
            var nextColumn = column;
            if (row == -depth) // bottom row
            {
                nextColumn++;
                if (column == depth) // right corner
                {
                    nextDepth++;
                }
            }
            else if (row == depth) // top row
            {
                if (column == -depth) // left corner
                {
                    nextRow--;
                }
                else
                {
                    nextColumn--;
                }
            }
            else if (column == depth) // right middle
            {
                nextRow++;
            }
            else // left middle
            {
                nextRow--;
            }

            return (nextDepth, nextRow, nextColumn);
        }
    }
}