using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Run(() =>
        {

            var components = (
                from line in File.ReadLines("input.txt")
                select Component.Parse(line) into component
                orderby component.Smallest
                select component
            ).ToImmutableList();
            return Bridge.Longest(components);
        });
        Run(() =>
        {

            var components = (
                from line in File.ReadLines("input.txt")
                select Component.Parse(line) into component
                orderby component.Smallest
                select component
            ).ToImmutableList();
            return Bridge.Strongest(components);
        });
    }

    static void Run<T>(Func<T> f)
    {
        var sw = Stopwatch.StartNew();
        var result = f();
        Console.WriteLine($"{result} - {sw.Elapsed}");
    }
}
