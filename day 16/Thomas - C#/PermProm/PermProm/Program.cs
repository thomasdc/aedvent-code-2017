using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PermProm
{
    public class Program
    {
        public static IDictionary<string, int> Lookups;

        private static void InitLookups()
        {
            Lookups = new Dictionary<string, int>();
            foreach (var ascii in Enumerable.Range(97, 16))
            {
                Lookups[Convert.ToChar(ascii).ToString()] = ascii - 97;
            }
        }

        static Program()
        {
            InitLookups();
        }

        public static string ToChars(int[] positions)
        {
            return string.Join("", positions.Select(_ => Convert.ToChar(97 + _)));
        }

        public static int[] FromChars(string chars)
        {
            return chars.Select(_ => Lookups[_.ToString()]).ToArray();
        }
        
        static void Main(string[] args)
        {
            var stopwatch = Stopwatch.StartNew();
            Console.WriteLine(Part2(Parse("input.txt")));
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        public static string Part1(IDanceMove[] moves)
        {
            var indexOf = Enumerable.Range(0, 16).ToArray();
            var positions = Enumerable.Range(0, 16).ToArray();
            foreach (var move in moves)
            {
                move.Move(indexOf, positions);
            }    
            
            return ToChars(positions);
        }
        
        public static string Part2(IDanceMove[] moves)
        {
            var indexOf = Enumerable.Range(0, 16).ToArray();
            var positions = Enumerable.Range(0, 16).ToArray();
            var positionsSeen = new List<string>();
            const int oneBillion = 1_000_000_000;
            for (var i = 0; i < oneBillion; i++)
            {
                foreach (var move in moves)
                {
                    move.Move(indexOf, positions);
                }

                var position = ToChars(positions);
                if (positionsSeen.Contains(position))
                {
                    Debug.Assert(positionsSeen.IndexOf(position) == 0);
                    Console.WriteLine($"Found a cycle between iteration {i} and {positionsSeen.IndexOf(position)} starting with {position}");
                    var cycleLength = i - positionsSeen.IndexOf(position);
                    var positionOfOneBillionInCycle = (oneBillion-1 - positionsSeen.IndexOf(position)) % cycleLength;
                    return positionsSeen[positionOfOneBillionInCycle];
                }
                
                positionsSeen.Add(position);
            }
            
            return ToChars(positions);
        }
        
        private static IDanceMove[] Parse(string fileName)
        {
            var moves = new List<IDanceMove>();
            foreach (var move in File.ReadAllLines(fileName).Single().Split(","))
            {
                if (move.StartsWith("s"))
                {
                    moves.Add(new Spin(int.Parse(new string(move.Skip(1).ToArray()))));
                } 
                else if (move.StartsWith("x"))
                {
                    var split = new string(move.Skip(1).ToArray()).Split("/");
                    moves.Add(new Exchange(int.Parse(split[0]), int.Parse(split[1])));
                }
                else if (move.StartsWith("p"))
                {
                    var split = new string(move.Skip(1).ToArray()).Split("/");
                    moves.Add(new Partner(split[0], split[1]));
                }
            }

            return moves.ToArray();
        }
    }

    public interface IDanceMove
    {
        void Move(int[] indexOf, int[] positions);
    }

    public class Spin : IDanceMove {
        public int Size { get; }

        public Spin(int size)
        {
            Size = size;
        }

        public void Move(int[] indexOf, int[] positions)
        {
            for (var i = 0; i < indexOf.Length; i++)
            {
                indexOf[i] = (indexOf[i] + Size) % indexOf.Length;
                positions[indexOf[i]] = i;
            }
        }
    }

    public class Exchange : IDanceMove
    {
        public int PositionA { get; }
        public int PositionB { get; }

        public Exchange(int positionA, int positionB)
        {
            PositionA = positionA;
            PositionB = positionB;
        }
        
        public void Move(int[] indexOf, int[] positions)
        {
            var temp = indexOf[positions[PositionA]];
            indexOf[positions[PositionA]] = indexOf[positions[PositionB]];
            indexOf[positions[PositionB]] = temp;
            temp = positions[PositionA];
            positions[PositionA] = positions[PositionB];
            positions[PositionB] = temp;
        }
    }

    public class Partner : IDanceMove
    {
        public string ProgramA { get; }
        public string ProgramB { get; }

        public Partner(string programA, string programB)
        {
            ProgramA = programA;
            ProgramB = programB;
        }
        
        public void Move(int[] indexOf, int[] positions)
        {
            var programALookup = Program.Lookups[ProgramA];
            var programBLookup = Program.Lookups[ProgramB];
            var temp = positions[indexOf[programALookup]];
            positions[indexOf[programALookup]] = positions[indexOf[programBLookup]];
            positions[indexOf[programBLookup]] = temp;
            temp = indexOf[programALookup];
            indexOf[programALookup] = indexOf[programBLookup];
            indexOf[programBLookup] = temp;
        }
    }
}
