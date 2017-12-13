using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Plumber
{
    class Program
    {
        static void Main(string[] args)
        {
            var edges = (
                from line in File.ReadLines("input.txt")
                let parts = line.Split("<->").Select(s => s.Trim()).ToArray()
                let parent = int.Parse(parts[0])
                from child in parts[1].Split(',').Select(int.Parse).ToArray()
                select (parent: parent, child: child)
            ).ToList();

            var lookup = edges.ToLookup(x => x.parent);

            var counts = new Dictionary<int, HashSet<int>>();

            foreach (var (parent, _) in edges)
            {
                if (counts.Any(h => h.Value.Contains(parent))) continue;
                var values = new[] { parent };
                var hash = new HashSet<int>();
                counts[parent] = hash;
                while (true)
                {
                    var parents = values.Where(n => !hash.Contains(n)).ToArray();
                    if (!parents.Any()) break;
                    foreach (var p in parents) hash.Add(p);
                    values = parents.SelectMany(n => lookup[n]).Select(x => x.child).ToArray();
                }
            }

            Console.WriteLine(counts[0].Count);
            Console.WriteLine(counts.Count());
        }

    }

}
