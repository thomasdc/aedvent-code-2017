using System;
using System.IO;
using System.Linq;

namespace Hex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part1(File.ReadAllText("input.txt").Split(",")));
        }

        private static int Part1(string[] directions)
        {
            var x = 0;
            var y = 0;
            var z = 0;
            foreach (var direction in directions)
            {
                (x, y, z) = Move(x, y, z, direction);
            }

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
