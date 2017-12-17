using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Run(() => 0);
    }
    
    static void Run<T>(Func<T> f)
    {
        var sw = Stopwatch.StartNew();
        var result = f();
        Console.WriteLine($"{result} - {sw.Elapsed}");
    }
}
