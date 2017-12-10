using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Knots
{
    class Program
    {
        static void Main(string[] args)
        {

            Run(() =>
            {
                var input = new byte[] { 206, 63, 255, 131, 65, 80, 238, 157, 254, 24, 133, 2, 16, 0, 1, 3 };
                var result = KnotsHash.Hash(input);
                var value = result[0] * result[1];
                return value;
            });

            Run(() =>
            {
                var input = "206,63,255,131,65,80,238,157,254,24,133,2,16,0,1,3";
                var result = KnotsHash.Hash(input);
                return result;
            });

        }

        static void Run<T>(Func<T> f)
        {
            var sw = Stopwatch.StartNew();
            var result = f();
            Console.WriteLine($"{result} in {sw.Elapsed}");
        }
    }
}
