using System;
using System.Diagnostics;
using System.IO;

namespace Garbage
{
    class Program
    {
        static void Main(string[] args)
        {
            Run(() =>
                {
                    using (var streamReader = new StreamReader(File.OpenRead("input.txt")))
                    {
                        var result = new GarbageProcessor().Process(streamReader);
                        return result;
                    }
                }
            );
        }

        static void Run<T>(Func<T> f)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = f();
            Console.WriteLine($"{result} in {stopwatch.Elapsed}");
        }
    }
}
