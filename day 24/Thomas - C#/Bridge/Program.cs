using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part2(Parse("input.txt").ToArray()));
            Console.ReadLine();
        }

        private static IEnumerable<Component> Parse(string fileName)
        {
            foreach (var line in File.ReadAllLines(fileName))
            {
                var split = line.Split("/");
                yield return new Component(int.Parse(split[0]), int.Parse(split[1]));
            }
        }

        public static int Part1(Component[] components)
        {
            var initialPort = 0;
            var bridges = EnumerateBridges(initialPort, components, new Stack<Component>());
            /*foreach (var bridge in bridges)
            {
                Console.WriteLine(string.Join("--", bridge.Select(component => $"{component.Ports[0].PinType}/{component.Ports[1].PinType}")));
            }*/

            return SumOfStrength(bridges.OrderByDescending(SumOfStrength).First());
        }

        public static int Part2(Component[] components)
        {
            var initialPort = 0;
            var bridges = EnumerateBridges(initialPort, components, new Stack<Component>());
            return SumOfStrength(bridges.OrderByDescending(_ => _.Length).ThenByDescending(SumOfStrength).First());
        }

        private static int SumOfStrength(IList<Component> bridge)
        {
            return bridge.Sum(component => component.Strength);
        }

        private static IEnumerable<Component[]> EnumerateBridges(int portToMatch, IList<Component> unvisitedNodes, Stack<Component> currentBranch)
        {
            var nextPortToMatch = -1;
            foreach (var matchingComponent in unvisitedNodes.Where(_ => _.Match(portToMatch, out nextPortToMatch)))
            {
                currentBranch.Push(matchingComponent);
                var newUnvisitedNodes = unvisitedNodes.Where(_ => _ != matchingComponent).ToList();
                yield return currentBranch.Reverse().ToArray();

                foreach (var match in EnumerateBridges(nextPortToMatch, newUnvisitedNodes, currentBranch))
                {
                    yield return match;
                }

                currentBranch.Pop();
            }
        }

        internal class Component
        {
            public int[] Ports { get; }
            public int Strength => Ports.Sum(_ => _);

            public Component(int portA, int portB)
            {
                Ports = new[] {portA, portB};
            }

            public bool Match(int otherPort, out int nextPortToMatch)
            {
                nextPortToMatch = -1;
                for (var i = 0; i < 2; i++)
                {
                    var port = Ports[i];
                    if (port == otherPort)
                    {
                        nextPortToMatch = Ports[(i + 1) % 2];
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
