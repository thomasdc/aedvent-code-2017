﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Knot
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.WriteLine(Part2(256, "34,88,2,222,254,93,150,0,199,255,39,32,137,136,1,167", 64));
        }

        private static int Part1(int size, int[] lengths)
        {
            return Hash(size, lengths, 1).Take(2).Aggregate((x, y) => x * y);
        }

        private static string Part2(int size, string input, int numberOfRounds)
        {
            var lengths = input.Select(_ => Encoding.ASCII.GetBytes(_.ToString()).Select(@byte => Convert.ToString(@byte))
                .ToList()).SelectMany(_ => _).Concat(new List<string> { "17", "31", "73", "47", "23" }).Select(int.Parse).ToArray();

            var sparseHash = Hash(size, lengths, numberOfRounds);
            const int blockSize = 16;
            var denseHash = new int[blockSize];
            for (var blockIndex = 0; blockIndex < 16; blockIndex++)
            {
                var numbers = sparseHash.Skip(blockIndex * blockSize).Take(blockSize);
                denseHash[blockIndex] = numbers.Aggregate((x, y) => x ^ y);
            }

            return Densify(denseHash);
        }

        private static int[] Hash(int size, int[] lengths, int numberOfRounds)
        {
            var values = Enumerable.Range(0, size).ToArray();
            var skipSize = 0;
            var currentPosition = 0;

            for (var round = 0; round < numberOfRounds; round++)
            {
                foreach (var length in lengths)
                {
                    for (var i = 0; i < length / 2; i++)
                    {
                        var startIndex = (currentPosition + i) % values.Length;
                        var endIndex = (currentPosition + length - 1 - i) % values.Length;
                        var temp = values[startIndex];
                        values[startIndex] = values[endIndex];
                        values[endIndex] = temp;
                    }
                    currentPosition = (currentPosition + length + skipSize++) % values.Length;
                }
            }

            return values;
        }
        
        private static string Densify(int[] input)
        {
            return string.Join("", input.Select(_ => _.ToString("X2"))).ToLowerInvariant();
        }
    }
}
