using System;
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
            Console.WriteLine(Part2("ljoxqyyw"));
        }

        private static int Part1(string input)
        {
            return Enumerable.Range(0, 128).Sum(row =>
                KnotHash($"{input}-{row}").Select(hashChar => int.Parse(hashChar.ToString(), NumberStyles.HexNumber))
                    .Sum(NumberOfBitsTurnedOn));
        }

        private static int Part2(string input)
        {
            var cells = ExtractLinkedCells(input);
            var groups = new List<HashSet<Cell>>();
            foreach (var cell in cells)
            {
                if (!groups.Any(_ => _.Contains(cell)))
                {
                    var group = ExpandGroup(cell, new HashSet<Cell>());
                    groups.Add(group);
                }
            }
            
            return groups.Count;
        }

        private static HashSet<Cell> ExpandGroup(Cell cell, HashSet<Cell> groupUntilNow)
        {
            if (groupUntilNow.Contains(cell))
            {
                return groupUntilNow;
            }

            groupUntilNow.Add(cell);

            foreach (var linkedCell in cell.AdjacentCells)
            {
                groupUntilNow.UnionWith(ExpandGroup(linkedCell, groupUntilNow));
            }

            return groupUntilNow;
        }

        private static bool AreNeighbours(Cell currentCell, Cell _)
        {
            return Math.Abs(currentCell.Row - _.Row) + Math.Abs(currentCell.Column - _.Column) == 1;
        }

        private static Cell[] ExtractLinkedCells(string input)
        {
            var matrix = ExtractMatrix(input);
            const int matrixSize = 128;
            var processedCells = new List<Cell>();
            for (var row = 0; row < matrixSize; row++)
            {
                for (var column = 0; column < matrixSize; column++)
                {
                    if (matrix[row][column])
                    {
                        var currentCell = new Cell(row, column);
                        foreach (var neighbouringCell in processedCells.Where(_ => AreNeighbours(currentCell, _)))
                        {
                            currentCell.LinkWith(neighbouringCell);
                        }
                        
                        processedCells.Add(currentCell);
                    }
                }
            }

            return processedCells.ToArray();
        }

        private static bool[][] ExtractMatrix(string input)
        {
            var matrix = new List<IList<bool>>();
            for (var rowIndex = 0; rowIndex < 128; rowIndex++)
            {
                var row = new List<bool>();
                var knotHash = KnotHash($"{input}-{rowIndex}");
                for (var group = 0; group < 32; group++)
                {
                    row.AddRange(ExtractBits(int.Parse(knotHash[group].ToString(), NumberStyles.HexNumber)).Reverse());
                }
                
                matrix.Add(row);
            }
            
            return matrix.Select(_ => _.ToArray()).ToArray();
        }

        private static IEnumerable<bool> ExtractBits(int input)
        {
            return Enumerable.Range(0, 4).Select(shift => (input >> shift) % 2 == 1);
        }

        private static int NumberOfBitsTurnedOn(int input)
        {
            return ExtractBits(input).Count(_ => _);
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

        private class Cell
        {
            public int Row { get; }
            public int Column { get; }
            public IList<Cell> AdjacentCells { get; }

            public Cell(int row, int column)
            {
                Row = row;
                Column = column;
                AdjacentCells = new List<Cell>();
            }

            public void LinkWith(Cell cell)
            {
                AdjacentCells.Add(cell);
                cell.AdjacentCells.Add(this);
            }
        }
    }
}
