using System;
using System.IO;
using System.Linq;

namespace Maze
{
    class Program
    {
        static void Main(string[] args)
        {
            var offsets = File.ReadAllLines("input.txt").Select(int.Parse).ToArray();
            Console.WriteLine(Part2(offsets));
        }
        
        public static int Part1(int[] offsets)
        {
            var numberOfJumps = 0;
            var nextIndex = 0;
            do
            {
                numberOfJumps++;
                nextIndex = nextIndex + offsets[nextIndex]++;
            } while (nextIndex >= 0 && nextIndex < offsets.Length);

            return numberOfJumps;
        }
        
        public static int Part2(int[] offsets)
        {
            var numberOfJumps = 0;
            var nextIndex = 0;
            do
            {
                numberOfJumps++;
                var offset = offsets[nextIndex];
                if (offset > 2)
                {
                    offsets[nextIndex]--;
                }
                else
                {
                    offsets[nextIndex]++;
                }
                nextIndex = nextIndex + offset;
            } while (nextIndex >= 0 && nextIndex < offsets.Length);

            return numberOfJumps;
        }
    }
}
