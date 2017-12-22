using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part1(703, 516));
        }

        private static int Part1(int input1, int input2)
        {
            const int numberOfIterations = 40_000_000;
            return Iterate(input1, 16807).Take(numberOfIterations).Zip(Iterate(input2, 48271).Take(numberOfIterations), (a, b) => BitsMatch(a, b) ? 1 : 0).Sum();
        }

        private static IEnumerable<long> Iterate(long start, int multiplicationFactor)
        {
            var next = start;
            while (true)
            {
                next = (next * multiplicationFactor) % int.MaxValue;
                yield return next;
            }
        }

        private static bool BitsMatch(long a, long b)
        {
            return a << 48 == b << 48;
        }
    }
}
