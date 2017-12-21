using System;
using System.IO;
using System.Linq;

namespace Firewall
{
    public class Program
    {
        static void Main(string[] args)
        {
            var layers = Parse(File.ReadAllLines("input.txt"));
            Console.WriteLine(Part2(layers));
        }

        private static int Part1((int depth, int range)[] layers)
        {
            return layers.Where(layer => Caught(layer.range, layer.depth)).Sum(layer => layer.depth * layer.range);
        }
        
        private static int Part2((int depth, int range)[] layers)
        {
            return Enumerable.Range(0, int.MaxValue).First(delay => !layers.Any(layer => Caught(layer.range, layer.depth + delay)));
        }

        private static (int depth, int range)[] Parse(string[] input)
        {
            return input.Select(inputLine => inputLine.Split(": "))
                .Select(split => (int.Parse(split[0]), int.Parse(split[1])))
                .ToArray();
        }

        public static bool Caught(int range, int picosecond)
        {
            return picosecond % (range * 2 - 2) == 0;
        }
    }
}
