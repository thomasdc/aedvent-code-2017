using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Run(() =>
        {
            var rules = File.ReadLines("input.txt").Select(Rule.Parse).ToArray();
            var input = ".#.\r\n..#\r\n###".ReadLines().ToRectangular();
            var expandingGrid = new ExpandingGrid(input);
            for (int i = 0; i < 5; i++)
            {
                expandingGrid = expandingGrid.Expand(rules);
            }
            Console.WriteLine(expandingGrid);

            return expandingGrid.Count('#');
        });

        Run(() =>
        {
            var rules = File.ReadLines("input.txt").Select(Rule.Parse).ToArray();
            var input = ".#.\r\n..#\r\n###".ReadLines().ToRectangular();
            var expandingGrid = new ExpandingGrid(input);
            for (int i = 0; i < 18; i++)
            {
                expandingGrid = expandingGrid.Expand(rules);
            }
            return expandingGrid.Count('#');
        });

    }



    static void Run<T>(Func<T> f)
    {
        var sw = Stopwatch.StartNew();
        var result = f();
        Console.WriteLine($"{result} - {sw.Elapsed}");
    }
}
