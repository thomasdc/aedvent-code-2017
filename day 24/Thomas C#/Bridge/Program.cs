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
            Console.WriteLine(Part1(Parse("input.txt").ToArray()));
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
            var initialPort = new Port(null, 0);
            var bridges = EnumerateBridges(initialPort, components, new Stack<Component>());
            /*foreach (var bridge in bridges)
            {
                Console.WriteLine(string.Join("--", bridge.Select(component => $"{component.Ports[0].PinType}/{component.Ports[1].PinType}")));
            }*/

            return SumOfStrength(bridges.OrderByDescending(SumOfStrength).First());
        }

        private static int SumOfStrength(IList<Component> bridge)
        {
            return bridge.Sum(component => component.Strength);
        }

        private static IEnumerable<Component[]> EnumerateBridges(Port portToMatch, IList<Component> unvisitedNodes, Stack<Component> currentBranch)
        {
            Port nextPortToMatch = null;
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
            public Port[] Ports { get; }
            public int Strength => Ports.Sum(_ => _.PinType);

            public Component(int portA, int portB)
            {
                Ports = new[] {new Port(this, portA), new Port(this, portB)};
            }

            public bool Match(Port other, out Port nextPortToMatch)
            {
                nextPortToMatch = null;
                for (var i = 0; i < 2; i++)
                {
                    var port = Ports[i];
                    if (port.Match(other))
                    {
                        nextPortToMatch = Ports[(i + 1) % 2];
                        return true;
                    }
                }

                return false;
            }

            public bool IsStrongerThan(Component other)
            {
                return Strength > other.Strength;
            }
        }

        internal class Port
        {
            public int PinType { get; }
            public Component Component { get; }
            public Port LinkedToPort { get; private set; }

            public Port(Component component, int pinType)
            {
                Component = component;
                PinType = pinType;
            }

            public bool Match(Port other)
            {
                if (PinType != other.PinType) return false;
                LinkedToPort = other;
                other.LinkedToPort = this;
                return true;
            }
            
            public void Unlink()
            {
                LinkedToPort.LinkedToPort = null;
                LinkedToPort = null;
            }
        }
    }
}
