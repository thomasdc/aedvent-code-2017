using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fractal
{
    class Program
    {
        static void Main(string[] args)
        {
            var rules = File.ReadAllLines("input.txt").Select(_ => new EnhancementRule(_)).ToList();
            var result = Iterate(ParseGrid(".#./..#/###"), rules, 5);
            Console.WriteLine(result);
            Console.ReadLine();
        }

        private static int Iterate(bool[,] grid, IList<EnhancementRule> rules, int numberOfIterations)
        {
            foreach (var iteration in Enumerable.Range(0, numberOfIterations))
            {
                var gridSize = grid.GetLength(0);
                var subGridSize = gridSize % 2 == 0 ? 2 : 3;
                var numberOfRowsAndColumns = gridSize / subGridSize;
                var enhancementRules = new List<EnhancementRule>();
                for (var x = 0; x < numberOfRowsAndColumns; x++)
                {
                    for (var y = 0; y < numberOfRowsAndColumns; y++)
                    {
                        var subGridXOffset = x * subGridSize;
                        var subGridYOffset = y * subGridSize;
                        var ruleToApply = rules.First(rule => Matches(grid, subGridXOffset, subGridYOffset, subGridSize, rule));
                        enhancementRules.Add(ruleToApply);
                    }
                }
                
                grid = Enhance(enhancementRules, numberOfRowsAndColumns);
            }

            return Sum(grid);
        }

        private static int Sum(bool[,] grid)
        {
            var sum = 0;
            for (var i = 0; i < grid.GetLength(0); i++)
            {
                for (var j = 0; j < grid.GetLength(0); j++)
                {
                    sum += grid[i, j] ? 1 : 0;
                }
            }

            return sum;
        }

        private static void Print(bool[,] grid)
        {
            var builder = new StringBuilder();
            for (var y = 0; y < grid.GetLength(1); y++)
            {
                for (var x = 0; x < grid.GetLength(0); x++)
                {
                    builder.Append($"{(grid[x, y] ? "#" : ".")} ");
                }

                builder.AppendLine();
            }

            Console.WriteLine(builder);
        }
        
        private static void RotateInPlace(bool[,] grid, int subGridXOffset, int subGridYOffset, int subGridSize)
        {
            var length = subGridSize - 1;
            for (var layer = 0; layer <= length / 2; layer++)
            {
                for (var x = layer; x < length - layer; x++)
                {
                    var temp = grid[x + subGridXOffset, layer + subGridYOffset];
                    grid[x + subGridXOffset, layer + subGridYOffset] = grid[layer + subGridXOffset, subGridYOffset + length - x];
                    grid[layer + subGridXOffset, subGridYOffset + length - x] = grid[subGridXOffset + length - x, subGridYOffset + length - layer];
                    grid[subGridXOffset + length - x, subGridYOffset + length - layer] = grid[subGridXOffset + length - layer, subGridYOffset + x];
                    grid[subGridXOffset + length - layer, subGridYOffset + x] = temp;
                }
            }
        }

        private static void FlipXInPlace(bool[,] grid, int subGridXOffset, int subGridYOffset, int subGridSize)
        {
            for (var x = 0; x < subGridSize; x++)
            {
                for (var y = 0; y < subGridSize / 2; y++)
                {
                    var xVal = x + subGridXOffset;
                    var temp = grid[xVal, y + subGridYOffset];
                    var yVal = subGridSize - 1 - y + subGridYOffset;
                    grid[xVal, y + subGridYOffset] = grid[xVal, yVal];
                    grid[xVal, yVal] = temp;
                }
            }
        }

        private static void FlipYInPlace(bool[,] grid, int subGridXOffset, int subGridYOffset, int subGridSize)
        {
            for (var y = 0; y < subGridSize; y++)
            {
                for (var x = 0; x < subGridSize / 2; x++)
                {
                    var yVal = y + subGridYOffset;
                    var temp = grid[x + subGridXOffset, yVal];
                    var xVal = subGridSize - 1 - x + subGridXOffset;
                    grid[x + subGridXOffset, yVal] = grid[xVal, yVal];
                    grid[xVal, yVal] = temp;
                }
            }
        }

        private static bool[,] Enhance(IList<EnhancementRule> rulesToApply, int numberOfRowsAndColumns)
        {
            var enhancedGridSize = rulesToApply.First().EnhancedGridSize * numberOfRowsAndColumns;
            var grid = new bool[enhancedGridSize, enhancedGridSize];

            for (var ruleX = 0; ruleX < numberOfRowsAndColumns; ruleX++)
            {
                for (var ruleY = 0; ruleY < numberOfRowsAndColumns; ruleY++)
                {
                    var currentRule = rulesToApply[ruleX * numberOfRowsAndColumns + ruleY];
                    var xOffset = ruleX * currentRule.EnhancedGridSize;
                    var yOffset = ruleY * currentRule.EnhancedGridSize;
                    for (var x = 0; x < currentRule.EnhancedGridSize; x++)
                    {
                        for (var y = 0; y < currentRule.EnhancedGridSize; y++)
                        {
                            grid[xOffset + x, yOffset + y] = currentRule.EnhancedGrid[x, y];
                        }
                    }
                }
            }

            return grid;
        }

        private static bool Matches(bool[,] grid, int subGridXOffset, int subGridYOffset, int subGridSize, bool[,] subGridToMatch)
        {
            for (var x = 0; x < subGridSize; x++)
            {
                for (var y = 0; y < subGridSize; y++)
                {
                    if (grid[subGridXOffset + x, subGridYOffset + y] != subGridToMatch[x, y])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool Matches(bool[,] grid, int subGridXOffset, int subGridYOffset, int subGridSize, EnhancementRule rule)
        {
            if (subGridSize != rule.GridSize) return false;
            foreach (var rotation in Enumerable.Range(0, 4))
            {
                RotateInPlace(grid, subGridXOffset, subGridYOffset, subGridSize);
                if (Matches(grid, subGridXOffset, subGridYOffset, subGridSize, rule.Grid))
                {
                    return true;
                }

                FlipXInPlace(grid, subGridXOffset, subGridYOffset, subGridSize);
                if (Matches(grid, subGridXOffset, subGridYOffset, subGridSize, rule.Grid))
                {
                    return true;
                }

                // undo
                FlipXInPlace(grid, subGridXOffset, subGridYOffset, subGridSize);

                FlipYInPlace(grid, subGridXOffset, subGridYOffset, subGridSize);
                if (Matches(grid, subGridXOffset, subGridYOffset, subGridSize, rule.Grid))
                {
                    return true;
                }

                // undo
                FlipYInPlace(grid, subGridXOffset, subGridYOffset, subGridSize);
            }

            return false;
        }

        struct EnhancementRule
        {
            public bool[,] Grid { get; }
            public bool[,] EnhancedGrid { get; }
            public int GridSize => Grid.GetLength(0);
            public int EnhancedGridSize => EnhancedGrid.GetLength(0);
            
            public EnhancementRule(string input)
            {
                var rawParts = input.Split(" => ");
                Grid = ParseGrid(rawParts[0]);
                EnhancedGrid = ParseGrid(rawParts[1]);
            }
        }

        private static bool[,] ParseGrid(string input)
        {
            var fromParts = input.Split('/');
            var size = fromParts.Length;
            var grid = new bool[size, size];
            for (var x = 0; x < size; x++)
            {
                for (var y = 0; y < size; y++)
                {
                    grid[x, y] = fromParts[y][x] == '#';
                }
            }

            return grid;
        }
    }
}
