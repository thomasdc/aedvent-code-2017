using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tower
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Parse(File.ReadAllLines("input.txt"));
            Console.WriteLine(Part1(input.programNames, input.relations));
        }

        private static (string[] programNames, KnownRelation[] relations) Parse(string[] inputLines)
        {
            var names = new HashSet<string>();
            var relations = new List<KnownRelation>();

            foreach (var line in inputLines)
            {
                var parts = line.Split(" ");
                var parentName = parts[0];
                names.Add(parentName);
                foreach (var childName in parts.Skip(3).Select(_ => _.Replace(",", string.Empty)))
                {
                    names.Add(childName);
                    relations.Add(new KnownRelation(parentName, childName));
                }
            }
            
            return (names.ToArray(), relations.ToArray());
        }

        private static string Part1(string[] programNames, KnownRelation[] relations)
        {
            var programs = programNames.Select(_ => new TowerProgram(_)).ToDictionary(_ => _.Name, _ => _);
            foreach (var relation in relations)
            {
                programs[relation.ChildProgramName].AssignParent(programs[relation.ParentProgramName]);
            }

            var program = programs.First().Value;
            while (program.HasParent)
            {
                program = program.ParentProgram;
            }
            
            return program.Name;
        }

        private class KnownRelation
        {
            public string ParentProgramName { get; }
            public string ChildProgramName { get; }
            
            public KnownRelation(string parentProgramName, string childProgramName)
            {
                ParentProgramName = parentProgramName;
                ChildProgramName = childProgramName;
            }
        }

        private class TowerProgram
        {
            public TowerProgram ParentProgram { get; private set; }
            public IList<TowerProgram> ChildPrograms { get; }
            public string Name { get; }
            public bool HasParent => ParentProgram != null;
            
            public TowerProgram(string name)
            {
                Name = name;
                ChildPrograms = new List<TowerProgram>();
            }

            public void AssignParent(TowerProgram parent)
            {
                ParentProgram = parent;
                parent.ChildPrograms.Add(this);
            }
        }
    }
}