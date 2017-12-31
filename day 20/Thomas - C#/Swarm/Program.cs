using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Swarm
{
    class Program
    {
        static void Main(string[] args)
        {
            var particles = Parse(File.ReadAllLines("input.txt")).ToList();
            Part1(particles);
        }

        private static void Part1(IList<Particle> particles)
        {
            while (true)
            {
                foreach (var particle in particles)
                {
                    particle.Tick();
                }

                Console.WriteLine(particles.OrderBy(_ => _.DistanceFromCenter()).First().Id);
            }
        }

        private static IEnumerable<Particle> Parse(string[] input)
        {
            var index = 0;
            foreach (var line in input)
            {
                var split = line.Split(' ');
                yield return new Particle(index++, ExtractTriple(split[0]), ExtractTriple(split[1]), ExtractTriple(split[2]));
            }
        }

        private static (long x, long y, long z) ExtractTriple(string input)
        {
            var split = input.Trim('p', 'v', 'a', '=', '<', '>', ',').Split(',');
            return (long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2]));
        }

        class Particle
        {
            public readonly int Id;
            public (long x, long y, long z) Position;
            public (long x, long y, long z) Velocity;
            public (long x, long y, long z) Acceleration;

            public Particle(int id, 
                (long x, long y, long z) position, 
                (long x, long y, long z) velocity, 
                (long x, long y, long z) acceleration)
            {
                Id = id;
                Position = position;
                Velocity = velocity;
                Acceleration = acceleration;
            }

            public void Tick()
            {
                Velocity.x += Acceleration.x;
                Velocity.y += Acceleration.y;
                Velocity.z += Acceleration.z;
                Position.x += Velocity.x;
                Position.y += Velocity.y;
                Position.z += Velocity.z;
            }

            public long DistanceFromCenter()
            {
                return Math.Abs(Position.x) + Math.Abs(Position.y) + Math.Abs(Position.z);
            }
        }
    }
}
