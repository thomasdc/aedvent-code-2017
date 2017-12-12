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
            Console.WriteLine(Part2(pipes));
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
            var group = Visit(programs.Single(_ => _.Name == "0"), new List<Programma>());
            return group.Count;
        }

        private static int Part2((string programA, string programB)[] pipes)
        {
            var programs = ExtractPrograms(pipes);
            var groups = EnumerateGroups(programs.ToList());
            return groups.Count;
        }

        private static IList<IList<Programma>> EnumerateGroups(IList<Programma> programs)
        {
            var groups = new List<IList<Programma>>();
            while (programs.Any())
            {
                var program = programs.First();
                var group = Visit(program, new List<Programma>());
                groups.Add(group);
                programs = programs.Where(x => group.All(y => y.Name != x.Name)).ToList();
            }
            return groups;
        }

        private static IList<Programma> Visit(Programma program, IList<Programma> visitedPrograms)
        {
            if (visitedPrograms.Contains(program))
            {
                return visitedPrograms;
            }
            
            visitedPrograms.Add(program);

            foreach (var pipedProgram in program.PipedPrograms)
            {                
                visitedPrograms.Concat(Visit(pipedProgram, visitedPrograms));
            }

            return visitedPrograms;
        }

        private static IList<Programma> ExtractPrograms((string programA, string programB)[] pipes)
        {
            var programs = new Dictionary<string, Programma>();
            foreach (var pipe in pipes)
            {
                if (!programs.ContainsKey(pipe.programA)) programs[pipe.programA] = new Programma(pipe.programA);
                if (!programs.ContainsKey(pipe.programB)) programs[pipe.programB] = new Programma(pipe.programB);
                programs[pipe.programA].PipedPrograms.Add(programs[pipe.programB]);
            }
            return programs.Values.ToList();
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
