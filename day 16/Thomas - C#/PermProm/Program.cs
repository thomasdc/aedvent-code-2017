using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PermProm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part1(Parse("input.txt")));
        }

        public static string Part1(IDanceMove[] moves)
        {
            var programs = InitPrograms();
            foreach (var move in moves)
            {
                move.Move(programs);
            }
            
            Array.Sort(programs, ComparePrograms);
            
            return string.Join("", programs.Select(_ => _.Character));
        }

        private static int ComparePrograms(Programma x, Programma y)
        {
            return x.Index - y.Index;
        }

        private static Programma[] InitPrograms()
        {
            return Enumerable.Range(97, 16).Select(_ => new Programma(Convert.ToChar(_).ToString(), _ - 97)).ToArray();
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

    class Programma
    {
        public string Character { get; set; }
        public int Index { get; set; }

        public Programma(string character, int index)
        {
            Character = character;
            Index = index;
        }
    }

    interface IDanceMove
    {
        void Move(Programma[] programs);
    }
    
    class Spin : IDanceMove {
        public int Size { get; }

        public Spin(int size)
        {
            Size = size;
        }
        
        public void Move(Programma[] programs)
        {
            foreach (var program in programs)
            {
                program.Index = (program.Index + Size) % 16;
            }
        }
    }

    class Exchange : IDanceMove
    {
        public int PositionA { get; }
        public int PositionB { get; }

        public Exchange(int positionA, int positionB)
        {
            PositionA = positionA;
            PositionB = positionB;
        }
        
        public void Move(Programma[] programs)
        {
            var programA = programs.First(_ => _.Index == PositionA);
            var programB = programs.First(_ => _.Index == PositionB);
            programA.Index = PositionB;
            programB.Index = PositionA;
        }
    }

    class Partner : IDanceMove
    {
        public string ProgramA { get; }
        public string ProgramB { get; }

        public Partner(string programA, string programB)
        {
            ProgramA = programA;
            ProgramB = programB;
        }
        
        public void Move(Programma[] programs)
        {
            var programA = programs.First(_ => _.Character == ProgramA);
            var programB = programs.First(_ => _.Character == ProgramB);
            programA.Character = ProgramB;
            programB.Character = ProgramA;
        }
    }
}
