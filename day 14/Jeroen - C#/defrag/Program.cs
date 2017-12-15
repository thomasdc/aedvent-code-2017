using System;
using System.Diagnostics;
using System.Numerics;

namespace defrag
{
    class Program
    {
        static void Main(string[] args)
        {

            int x = 0b10;

            Console.WriteLine(Convert.ToString(x, 2).PadLeft(9, '0'));
            Console.WriteLine(Convert.ToString(x << 1, 2).PadLeft(9, '0'));
            Console.WriteLine(Convert.ToString(x >> 1, 2).PadLeft(9, '0'));

            Defrag.CountBits(new BigInteger(1));

            string key = "hxtvlmkl";
            Run(() => Defrag.CountBitsInGrid(key));
            Run(() => Defrag.PartTwo(key));
        }

        static void Run<T>(Func<T> f)
        {
            var sw = Stopwatch.StartNew();
            var result = f();
            Console.WriteLine($"{result} - {sw.Elapsed}");
        }
    }
}
