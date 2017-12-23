using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MoreLinq;
using static System.Math;

class Program
{
    static void Main(string[] args)
    {
        var particles = File.ReadLines("input.txt").Select(Particle.Parse).ToArray();
        var t =  particles.Sum(p => Abs(p.Acceleration.x) + Abs(p.Acceleration.y) + Abs(p.Acceleration.z));

        Run(() =>
        {
            var result = (
                from x in particles.Select((p, i) => (p: p, i: i))
                let position = x.p.GetPosition(t)
                let distance = Abs(position.x) + Abs(position.y) + Abs(position.z)
                select (index: x.i, particle: x.p, position: position, distance: distance)
            ).MinBy(x => x.distance).index;
            return result;
        });

        Run(() =>
        {
            var workingset = particles.ToArray();
            for (int i = 0; i < 100; i++)
            {
                workingset = workingset.Select(p => p.Tick()).GroupBy(x => x.Position).Where(x => !x.Skip(1).Any()).Select(x => x.First()).ToArray();
            }
            return workingset.Length;
        });
    }
    
    static void Run<T>(Func<T> f)
    {
        var sw = Stopwatch.StartNew();
        var result = f();
        Console.WriteLine($"{result} - {sw.Elapsed}");
    }
}

static class Helpers
{
    public static IEnumerable<string> ReadLines(this string input)
    {
        using (var reader = new StringReader(input))
        {
            foreach (var line in reader.ReadLines()) yield return line;
        }
    }

    public static IEnumerable<string> ReadLines(this TextReader reader)
    {
        while (reader.Peek() >= 0)
        {
            yield return reader.ReadLine();
        }
    }
}
