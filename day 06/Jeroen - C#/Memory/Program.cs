﻿using System;
using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Memory
{
    // 10	3	15	10	5	15	5	15	9	2	5	8	5	2	3	6

    class Program
    {

        static void Main(string[] args)
        {
            Run(() => Memory.Cycles(new byte[]
            {
            }.ToImmutableArray()));
            Run(() => Memory.Cycles(new byte[] { 10, 3, 15, 10, 5, 15, 5, 15, 9, 2, 5, 8, 5, 2, 3, 6 }.ToImmutableArray()));
        }

        static void Run<T>(Func<T> f)
        {
            var sw = Stopwatch.StartNew();
            var result = f();
            Console.WriteLine($"{result} / {sw.Elapsed}");
        }
    }
}
