using System;
using System.Collections.Generic;
using System.Linq;

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
            return Cells().Skip(input-1).Select(_ => Math.Abs(_.column) + Math.Abs(_.row)).First();
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
    }
}