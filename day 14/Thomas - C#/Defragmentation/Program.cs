using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Defragmentation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part1("ljoxqyyw"));
        }

        private static int Part1(string input)
        {
            return Enumerable.Range(0, 128).Sum(row =>
                KnotHash($"{input}-{row}").Select(hashChar => int.Parse(hashChar.ToString(), NumberStyles.HexNumber))
                    .Sum(NumberOfBitsTurnedOn));
        }

        private static int NumberOfBitsTurnedOn(int input)
        {
            return Enumerable.Range(0, 4).Count(shift => (input >> shift) % 2 == 1);
        }
        
        private static string KnotHash(string input)
        {
            return Part2(256, input, 64);
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
