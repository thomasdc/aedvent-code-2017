using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Memory
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt").Split('\t').Select(int.Parse).ToArray();
            Console.WriteLine(Part1(input));
        }

        public static int Part1(int[] banks)
        {
            var cycleIndex = 0;
            var blockDistributions = new HashSet<string>();

            do
            {
                blockDistributions.Add(string.Join("", banks));
                var maxIndex = Array.IndexOf(banks, banks.Max());
                var numberOfBlocksToRedistribute = banks[maxIndex];
                banks[maxIndex] = 0;
                for (var i = 1; i <= numberOfBlocksToRedistribute; i++)
                {
                    banks[(maxIndex + i) % banks.Length]++;
                }
                cycleIndex++;
            } while (!blockDistributions.Contains(string.Join("", banks)));
            
            return cycleIndex;
        }
    }
}