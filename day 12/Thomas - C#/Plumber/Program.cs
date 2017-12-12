using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Plumber
{
    class Program
    {
        static void Main(string[] args)
        {
            var pipes = Parse(File.ReadAllLines("input.txt")).ToArray();
            Console.WriteLine(Part1(pipes));
        }

        private static IEnumerable<(string programA, string programB)> Parse(string[] inputLines)
        {
            foreach (var inputLine in inputLines)
            {
                var parsedLine = inputLine.Split(" ");
                var programA = parsedLine[0];
                foreach (var programB in parsedLine.Skip(2).Select(_ => _.Trim(',')))
                {
                    yield return (programA, programB);
                }
            }
        }

        private static int Part1((string programA, string programB)[] pipes)
        {
            var programs = ExtractPrograms(pipes);
            var group = Visit(programs["0"], new List<string>());
            return group.Count;
        }

        private static IList<string> Visit(Programma program, IList<string> visitedGroups)
        {
            if (visitedGroups.Contains(program.Name))
            {
                return visitedGroups;
            }
            
            visitedGroups.Add(program.Name);

            foreach (var pipedProgram in program.PipedPrograms)
            {                
                visitedGroups.Concat(Visit(pipedProgram, visitedGroups));
            }

            return visitedGroups;
        }

        private static IDictionary<string, Programma> ExtractPrograms((string programA, string programB)[] pipes)
        {
            var programs = new Dictionary<string, Programma>();
            foreach (var pipe in pipes)
            {
                if (!programs.ContainsKey(pipe.programA)) programs[pipe.programA] = new Programma(pipe.programA);
                if (!programs.ContainsKey(pipe.programB)) programs[pipe.programB] = new Programma(pipe.programB);
                programs[pipe.programA].PipedPrograms.Add(programs[pipe.programB]);
            }
            return programs;
        }

        private class Programma
        {
            public string Name { get; }
            public readonly IList<Programma> PipedPrograms = new List<Programma>();

            public Programma(string name)
            {
                Name = name;
            }
        }
    }
}
