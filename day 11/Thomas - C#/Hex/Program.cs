using System;
using System.IO;
using System.Linq;

namespace Hex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part2(File.ReadAllText("input.txt").Split(",")));
        }

        private static int Part1(string[] directions)
        {
            return TakeSteps(directions, out _);
        }

        private static int Part2(string[] directions)
        {
            TakeSteps(directions, out var maxDistance);
            return maxDistance;
        }
        
        private static int TakeSteps(string[] directions, out int maxDistance)
        {
            int x = 0, y = 0, z = 0;
            maxDistance = int.MinValue;
            foreach (var direction in directions)
            {
                (x, y, z) = Move(x, y, z, direction);
                maxDistance = Math.Max(maxDistance, Distance(x, y, z));
            }

            return Distance(x, y, z);
        }

        private static int Distance(int x, int y, int z)
        {
            return new [] {Math.Abs(x), Math.Abs(y), Math.Abs(z)}.Max();
        }

        private static (int x, int y, int z) Move(int x, int y, int z, string direction)
        {
            if (direction == "n") return (x, y + 1, z - 1);
            if (direction == "nw") return (x - 1, y + 1, z);
            if (direction == "sw") return (x - 1, y, z + 1);
            if (direction == "s") return (x, y - 1, z + 1);
            if (direction == "se") return (x + 1, y - 1, z);
            if (direction == "ne") return (x + 1, y, z - 1);
            throw new Exception("I'm afraid I cannot do that Dave");
        }
    }
}
