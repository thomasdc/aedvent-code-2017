using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string[] instructions = File.ReadAllLines("input.txt");
        Run(() => new CPU1().Run(instructions));
        Run(() =>
        {
            var collection1 = new Queue<long>();
            var collection2 = new Queue<long>();
            var cpu1 = new CPU2(0, collection1, collection2);
            var cpu2 = new CPU2(1, collection2, collection1);
            cpu1.Load(instructions);
            cpu2.Load(instructions);
            do
            {
                cpu2.Run();
                cpu1.Run();
            } while (cpu2.HasIncoming);
            return cpu2.Sent;
        });
    }
    static void Run<T>(Func<T> f)
    {
        var sw = Stopwatch.StartNew();
        var result = f();
        Console.WriteLine($"{result} - {sw.Elapsed}");
    }
}

