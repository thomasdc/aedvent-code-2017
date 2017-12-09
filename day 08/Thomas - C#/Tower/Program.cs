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
            Console.WriteLine(Part2(input.programNames, input.programWeights, input.relations));
        }

        private static (string[] programNames, IDictionary<string, int> programWeights, KnownRelation[] relations) Parse(string[] inputLines)
        {
            var names = new HashSet<string>();
            var weights = new Dictionary<string, int>();
            var relations = new List<KnownRelation>();

            foreach (var line in inputLines)
            {
                var parts = line.Split(" ");
                var parentName = parts[0];
                names.Add(parentName);
                var weight = int.Parse(parts[1].Replace("(", string.Empty).Replace(")", string.Empty));
                weights.Add(parentName, weight);
                foreach (var childName in parts.Skip(3).Select(_ => _.Replace(",", string.Empty)))
                {
                    names.Add(childName);
                    relations.Add(new KnownRelation(parentName, childName));
                }
            }
            
            return (names.ToArray(), weights, relations.ToArray());
        }

        private static string Part1(string[] programNames, IDictionary<string, int> programWeights, KnownRelation[] relations)
        {
            var root = GetRootProgram(programNames, programWeights, relations);
            return root.Name;
        }

        private static int Part2(string[] programNames, IDictionary<string, int> programWeights, KnownRelation[] relations)
        {
            var root = GetRootProgram(programNames, programWeights, relations);
            // total weights of children match
            // but... total weight of current program doesn't match its siblings total weight
            var childrenWeightMatches = false;
            var currentProgram = root;
            var totalWeightToMatch = -1;
            
            while (!childrenWeightMatches)
            {
                var countByTotalWeight = currentProgram.ChildPrograms.GroupBy(_ => _.TotalWeight).ToList();
                childrenWeightMatches = countByTotalWeight.Count == 1;

                if (!childrenWeightMatches)
                {
                    var anomalyWeightGrouping = countByTotalWeight.OrderBy(_ => _.Count()).First();
                    totalWeightToMatch = countByTotalWeight.OrderBy(_ => _.Count()).Skip(1).First().Key;
                    var anomalyProgram = anomalyWeightGrouping.Single();
                    currentProgram = anomalyProgram;    
                }
            }
            
            return totalWeightToMatch - currentProgram.TotalWeightOfChildren;
        }

        private static TowerProgram GetRootProgram(string[] programNames, IDictionary<string, int> programWeights, KnownRelation[] relations)
        {
            var programs = programNames.Select(_ => new TowerProgram(_, programWeights[_])).ToDictionary(_ => _.Name, _ => _);
            foreach (var relation in relations)
            {
                programs[relation.ChildProgramName].AssignParent(programs[relation.ParentProgramName]);
            }

            var program = programs.First().Value;
            while (program.HasParent)
            {
                program = program.ParentProgram;
            }

            return program;
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
            public int OwnWeight { get; }
            public bool HasParent => ParentProgram != null;
            public int TotalWeightOfChildren => ChildPrograms.Sum(_ => _.TotalWeight);
            public int TotalWeight => OwnWeight + TotalWeightOfChildren;
            
            public TowerProgram(string name, int ownWeight)
            {
                Name = name;
                OwnWeight = ownWeight;
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