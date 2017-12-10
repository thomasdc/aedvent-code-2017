using System.Diagnostics;
using System.Linq;

namespace Knot
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.WriteLine(Part1(256, new[] { 34, 88, 2, 222, 254, 93, 150, 0, 199, 255, 39, 32, 137, 136, 1, 167 }));
        }

        private static int Part1(int size, int[] lengths)
        {
            var values = Enumerable.Range(0, size).ToArray();
            var skipSize = 0;
            var currentPosition = 0;
            foreach (var length in lengths)
            {
                for (var i = 0; i < length/2; i++)
                {
                    var startIndex = (currentPosition + i) % values.Length;
                    var endIndex = (currentPosition + length-1 - i) % values.Length;
                    var temp = values[startIndex];
                    values[startIndex] = values[endIndex];
                    values[endIndex] = temp;
                }
                currentPosition = (currentPosition + length + skipSize++) % values.Length;
            }

            return values.Take(2).Aggregate((x, y) => x*y);
        }
    }
}