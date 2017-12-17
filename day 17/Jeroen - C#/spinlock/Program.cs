using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Run(() =>
        {
            var result = Spinlock.Find(377, 2017);
            return result.buffer[result.index + 1];
        });
        Run(() =>
        {
            var result = Spinlock.Find(3, 2017);
            return result.buffer[1];
        });
        Run(() => Spinlock.FindFast(377, 50_000_000));
    }

    static void Run<T>(Func<T> f)
    {
        var sw = Stopwatch.StartNew();
        var result = f();
        Console.WriteLine($"{result} - {sw.Elapsed}");
    }
}
