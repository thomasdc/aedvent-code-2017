using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace Spiral
{
    static class Spiral
    {
        // Part 1
        public static int DistanceToOrigin(int square)
        {
            var (x, y) = ToEuclidean(square);
            return Abs(x) + Abs(y);
        }

        // Part 2
        public static IEnumerable<(int square, (int x, int y), int value)> SpiralValues()
        {
            var values = new Dictionary<(int, int), int> { [(0, 0)] = 1 };
            return new[] { (1, (0, 0), 1) }.Concat(
                from i in InfiniteSequence().Skip(1)
                let square = i + 1
                let coordinate = ToEuclidean(square)
                let value = GetAndStoreValue(coordinate)
                select (square, coordinate, value));

            int GetAndStoreValue((int x, int y) coordinate)
                => values[coordinate] = Neighbours(coordinate)
                    .Select(c => values.ContainsKey(c) ? values[c] : 0)
                    .Sum();
        }

        // given the square position, calculates the (x,y) coordinate
        // inspired by https://stackoverflow.com/a/3715915
        public static (int x, int y) ToEuclidean(int square)
        {
            var i = square - 1;
            var j = Round(Sqrt(i));
            int F(double l) => (int)((l + Pow(j, 2) - i - j % 2) * 0.5 * Pow(-1, j));
            var k = Abs(Pow(j, 2) - i) - j;
            return (F(k), F(-k));
        }

        static IEnumerable<(int, int)> Neighbours((int x, int y) c)
            => new[]
            {
                (c.x - 1, c.y - 1),
                (c.x, c.y - 1),
                (c.x + 1, c.y - 1),
                (c.x + 1, c.y),
                (c.x + 1, c.y + 1),
                (c.x, c.y + 1),
                (c.x - 1, c.y + 1),
                (c.x - 1, c.y)
            };

        static IEnumerable<int> InfiniteSequence()
        {
            for (int i = 0; i < int.MaxValue; i++) yield return i;
        }
    }
}