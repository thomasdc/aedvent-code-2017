using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace hexgrid
{
    static class HexGrid
    {
        public static (int distance, int max) Calculate(string[] steps)
        {
            var aggregate = steps.Aggregate(
                (coordinate: (x:0, y:0, z:0), max: int.MinValue),
                (tuple, s) => (
                    Step(tuple.coordinate, s),
                    Max(tuple.max, tuple.coordinate.Items().Max(Abs))
                )
            );

            var coordinate = aggregate.coordinate;

            var distance = coordinate.Items().Max(Abs);

            return (distance, aggregate.max);
        }
        private static (int x, int y, int z) Step((int x, int y, int z) coordinate, string step)
        {
            switch (step)
            {
                case "n":
                    return (coordinate.x, coordinate.y + 1, coordinate.z - 1);
                case "nw":
                    return (coordinate.x - 1, coordinate.y + 1, coordinate.z);
                case "ne":
                    return (coordinate.x + 1, coordinate.y, coordinate.z - 1);
                case "s":
                    return (coordinate.x, coordinate.y - 1, coordinate.z + 1);
                case "sw":
                    return (coordinate.x - 1, coordinate.y, coordinate.z + 1);
                case "se":
                    return (coordinate.x + 1, coordinate.y - 1, coordinate.z);
                default:
                    throw new InvalidOperationException();
            }
        }

        static IEnumerable<T> Items<T>(this (T, T, T) tuple)
        {
            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
        }
    }
}