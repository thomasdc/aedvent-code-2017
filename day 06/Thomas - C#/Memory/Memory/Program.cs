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
            Console.WriteLine(Part2(input));
        }

        public static int Part1(int[] banks)
        {
            var cycleIndex = 0;
            var distributions = new HashSet<string>();
            
            foreach (var cycle in Cycles(banks))
            {
                if (distributions.Contains(cycle))
                {
                    return cycleIndex;
                }
                cycleIndex++;
                distributions.Add(cycle);
            }

            return -1;
        }
        
        public static int Part2(int[] banks)
        {
            var loopCount = 0;
            var distributions = new HashSet<string>();
            string cyclicDistribution = null;
            
            foreach (var cycle in Cycles(banks))
            {
                if (cyclicDistribution == null)
                {
                    if (distributions.Contains(cycle))
                    {
                        cyclicDistribution = cycle;
                    }
                    distributions.Add(cycle);
                }
                else
                {
                    loopCount++;
                    if (cycle == cyclicDistribution)
                    {
                        return loopCount;
                    }
                }
            }

            return -1;
        }

        public static IEnumerable<string> Cycles(int[] banks)
        {
            yield return string.Join("", banks);
            
            while (true)
            {
                var maxIndex = Array.IndexOf(banks, banks.Max());
                var numberOfBlocksToRedistribute = banks[maxIndex];
                banks[maxIndex] = 0;
                for (var i = 1; i <= numberOfBlocksToRedistribute; i++)
                {
                    banks[(maxIndex + i) % banks.Length]++;
                }

                yield return string.Join("", banks);
            }
        }
    }
}
