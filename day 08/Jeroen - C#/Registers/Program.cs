using System;
using System.IO;
using System.Linq;

namespace Registers
{
    class Program
    {
        static void Main(string[] args)
        {
            var instructions = File.ReadLines("input.txt").Select(Instruction.Parse).ToList();

            var memory = instructions.Select(i => i.Register).Distinct().ToDictionary(s => s, _ => 0);
            int m = int.MinValue;
            foreach (var i in instructions)
            {
                var r = i.Apply(memory);
                if (r > m) m = r;
            }

            Console.WriteLine(memory.Values.Max());
            Console.WriteLine(m);

        }
    }
}
