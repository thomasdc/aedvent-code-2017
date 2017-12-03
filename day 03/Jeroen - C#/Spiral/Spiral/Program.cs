using System;
using System.Linq;

namespace Spiral
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Spiral.DistanceToOrigin(265149));
            var solution = Spiral.SpiralValues().SkipWhile(i => i.value <= 265149).First();
            Console.WriteLine(solution);

        }
    }
}
