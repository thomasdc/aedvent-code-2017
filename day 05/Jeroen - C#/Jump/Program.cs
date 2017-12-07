using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

namespace Jump
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines("input.txt").Select(int.Parse).ToArray();
            Run(() => CalculateJumps(input, v => 1));
            Run(() => CalculateJumps(input, v => v >= 3 ? -1 : 1));
        }

        static void Run<T>(Func<T> f)
        {
            var sw = Stopwatch.StartNew();
            var result = f();
            Console.WriteLine($"{result} / {sw.Elapsed}");

        }

        static int CalculateJumps(ReadOnlySpan<int> input, Func<int, int> step)
        {
            var copy = input.ToArray();
            var steps = 0;
            var i = 0;
            while (i >= 0 && i < input.Length)
            {
                steps++;
                var j = i;
                var v = copy[i];
                i += v;
                copy[j] += step(v);
            }
            return steps;
        }

        [Theory]
        [InlineData(5, 0, 3, 0, 1, -3)]
        public void Test1(int expected, params int[] input)
        {
            var jumps = CalculateJumps(input, _ => 1);
            Assert.Equal(expected, jumps);
        }
        [Theory]
        [InlineData(10, 0, 3, 0, 1, -3)]
        public void Test2(int expected, params int[] input)
        {
            var jumps = CalculateJumps(input, v => v >= 3 ? -1 : 1);
            Assert.Equal(expected, jumps);
        }
    }

}
