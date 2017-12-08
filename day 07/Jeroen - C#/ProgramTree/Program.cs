using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ProgramTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Run(() =>
            {
                var tree = Tree.Parse(File.ReadAllText("input.txt"));

                Console.WriteLine($"part 1: {tree.Root.Label}");

                var invalidNode = (
                    from n in tree.AllNodes()
                    where !n.HasValidWeight && n.Children.All(x => x.HasValidWeight)
                    from child in n.Children
                    group child by child.Weight
                    into g
                    where g.Count() == 1
                    select g.Single()
                ).SingleOrDefault();

                var sibling = invalidNode.Siblings.First();

                var difference = invalidNode.Weight - sibling.Weight;

                Console.WriteLine($"part 2: {invalidNode.PrivateWeight - difference}");
            });
        }

        static void Run(Action a)
        {
            var sw = Stopwatch.StartNew();
            a();
            Console.WriteLine(sw.Elapsed);
        }
    }
}
