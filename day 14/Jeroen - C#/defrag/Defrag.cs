using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Knots;

namespace defrag
{
    static class Defrag
    {
        internal static int CountBits(uint value)
        {
            var count = 0;
            while (value != 0)
            {
                count++;
                value &= value - 1;
            }
            return count;
        }

        public static int CountBitsInGrid(string key)
        {
            return (
                from i in Enumerable.Range(0, 128)
                let input = $"{key}-{i}"
                let hash = KnotsHash.Hash(input)
                from j in Enumerable.Range(0, 4)
                let offset = j * 8
                let value = Convert.ToUInt32($"0x{hash.Substring(offset, 8)}", 16)
                select CountBits(value)
            ).Sum();
        }

        public static int CountRegions(string key)
        {
            var grid = new bool[128, 128];

            var rows =
                from i in Enumerable.Range(0, 128)
                let input = $"{key}-{i}"
                select (i:i, row: KnotsHash.Hash(input).ToBinary());

            foreach ((int row, string hash) in rows)
            {
                foreach (var col in Enumerable.Range(0, 128))
                    grid[row, col] = hash[col] == '1';
            }

            var count = 0;

            for (var x = grid.GetLowerBound(0); x <= grid.GetUpperBound(0); x++)
            for (var y = grid.GetLowerBound(1); y <= grid.GetUpperBound(1); y++)
            {
                if (grid[x, y])
                {
                    ClearGroup(x, y, grid);
                    count++;
                }
            }

            return count;
        }


        private static void ClearGroup(int x, int y, bool[,] diskBits)
        {
            diskBits[x, y] = false;

            foreach (var t in GetNeighbors(x,y))
            {
                if (t.x >= 0 && t.x < 128 && t.y >= 0 && t.y < 128 && diskBits[t.x, t.y])
                {
                    ClearGroup(t.x, t.y, diskBits);
                }
            }
        }

        public static IEnumerable<(int x, int y)> GetNeighbors(int x, int y)
        {
            yield return (x - 1, y);
            yield return (x + 1, y);
            yield return (x, y - 1);
            yield return (x, y + 1);
        }
    }


    static class Ex
    {
        public static string ToBinary(this string hex)
        {
            var sb = new StringBuilder();

            foreach (var c in hex.ToCharArray())
            {
                var v = int.Parse(c.ToString(), NumberStyles.HexNumber);
                sb.Append(Convert.ToString(v, 2).PadLeft(4, '0'));
            }

            return sb.ToString();
        }
    }
}
