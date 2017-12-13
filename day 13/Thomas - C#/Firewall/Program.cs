using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Firewall
{
    class Program
    {
        static void Main(string[] args)
        {
            var layers = Parse(File.ReadAllLines("input.txt"));
            Console.WriteLine(Part1(layers));
        }

        private static Layer[] Parse(string[] input)
        {
            return input.Select(inputLine => inputLine.Split(": "))
                .Select(split => new Layer(int.Parse(split[0]), int.Parse(split[1])))
                .ToArray();
        }

        private static int Part1(Layer[] layers)
        {
            var numberOfTicks = layers.Last().Depth+1;
            var packet = new Packet();
            foreach (var picosecond in Enumerable.Range(0, numberOfTicks))
            {
                // situation at picosecond
                packet.Tick(picosecond, layers);
                Tick(layers);
                // situation at picosecond+1
            }

            return packet.Collisions.Sum(_ => _.Severity);
        }

        private static void Tick(Layer[] layers)
        {
            foreach (var layer in layers)
            {
                layer.Tick();
            }
        }

        private class Layer
        {
            public int Depth { get; }
            public int Range { get; }
            public int ScanLocation { get; private set; }
            private bool _goingUp;

            public Layer(int depth, int range)
            {
                Depth = depth;
                Range = range;
                ScanLocation = 0;
                _goingUp = false;
            }

            public void Tick()
            {
                if (_goingUp)
                {
                    ScanLocation--;
                    _goingUp = ScanLocation > 0;
                }
                else
                {
                    ScanLocation++;
                    _goingUp = ScanLocation == Range - 1;
                }
            }

            public bool CollidesWithPacketAt(int picosecond)
            {
                return Depth == picosecond && ScanLocation == 0;
            }
        }

        private class Packet
        {
            public int Location { get; private set; }
            public IList<Collision> Collisions = new List<Collision>();

            public void Tick(int picosecond, Layer[] layers)
            {
                var newCollissions = layers.Where(layer => layer.CollidesWithPacketAt(picosecond))
                    .Select(layer => new Collision(picosecond, layer)).ToList();
                Collisions = Collisions.Concat(newCollissions).ToList();
                Location++;
            }
        }

        private class Collision
        {
            public int Picosecond { get; }
            public Layer Layer { get; }
            public int Severity => Layer.Depth * Layer.Range;
            
            public Collision(int picosecond, Layer layer)
            {
                Picosecond = picosecond;
                Layer = layer;
            }
        }
    }
}
