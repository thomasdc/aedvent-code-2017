using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoCo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part2());
            Console.ReadLine();
        }

        private static int Part2()
        {
            var b = 109900;
            const int c = 126900;
            var h = 0;
            
            do
            {
                var f = true;
                for (var d = 2; d < b; d++)
                {
                    if (b % d == 0)
                    {
                        // f = "b is not a prime since it's can by divided by d"
                        f = false;
                        break;
                    }
                }

                if (!f)
                {
                    // h = "no prime" counter
                    h++;
                }

                if (b == c)
                {
                    break;
                }

                b += 17;
            } while (true); // 1000 loops: (126900-109900)/17

            return h;
        }

        private static int Part1()
        {
            var instructions = Parse(File.ReadAllLines("input.txt"));
            var registers = new Dictionary<string, int>();
            foreach (var @char in "abcdefgh".Select(_ => _.ToString()))
            {
                registers[@char] = 0;
            }

            var currentInstructionIndex = 0;
            while (currentInstructionIndex >= 0 && currentInstructionIndex < instructions.Length)
            {
                currentInstructionIndex = instructions[currentInstructionIndex].Apply(registers, currentInstructionIndex);
            }

            return Multiply.InvocationCounter;
        }

        private static Instruction[] Parse(string[] inputLines)
        {
            var instructions = new List<Instruction>();
            foreach (var line in inputLines)
            {
                var split = line.Split(' ');
                switch (split[0])
                {
                    case "set": instructions.Add(new Set(split[1], split[2])); break;
                    case "sub": instructions.Add(new Subtract(split[1], split[2])); break;
                    case "mul": instructions.Add(new Multiply(split[1], split[2])); break;
                    case "jnz": instructions.Add(new Jump(split[1], split[2])); break;
                }
            }

            return instructions.ToArray();
        }

        public interface Instruction
        {
            int Apply(IDictionary<string, int> registers, int currentInstructionIndex);
        }

        class Set : Instruction
        {
            private readonly string _x;
            private readonly Func<IDictionary<string, int>, int> _ySelector;

            public Set(string x, string y)
            {
                _x = x;
                _ySelector = AsNumberOrRegisterValue(y);
            }

            public int Apply(IDictionary<string, int> registers, int currentInstructionIndex)
            {
                registers[_x] = _ySelector(registers);
                return currentInstructionIndex + 1;
            }
        }

        class Subtract : Instruction
        {
            private readonly string _x;
            private readonly Func<IDictionary<string, int>, int> _ySelector;

            public Subtract(string x, string y)
            {
                _x = x;
                _ySelector = AsNumberOrRegisterValue(y);
            }

            public int Apply(IDictionary<string, int> registers, int currentInstructionIndex)
            {
                registers[_x] -= _ySelector(registers);
                return currentInstructionIndex + 1;
            }
        }

        class Multiply : Instruction
        {
            private readonly string _x;
            private readonly Func<IDictionary<string, int>, int> _ySelector;
            public static int InvocationCounter;

            public Multiply(string x, string y)
            {
                _x = x;
                _ySelector = AsNumberOrRegisterValue(y);
            }

            public int Apply(IDictionary<string, int> registers, int currentInstructionIndex)
            {
                InvocationCounter++;
                registers[_x] *= _ySelector(registers);
                return currentInstructionIndex + 1;
            }
        }

        class Jump : Instruction
        {
            private readonly Func<IDictionary<string, int>, int> _xSelector;
            private readonly Func<IDictionary<string, int>, int> _ySelector;

            public Jump(string x, string y)
            {
                _xSelector = AsNumberOrRegisterValue(x);
                _ySelector = AsNumberOrRegisterValue(y);
            }

            public int Apply(IDictionary<string, int> registers, int currentInstructionIndex)
            {
                if (_xSelector(registers) != 0)
                {
                    return currentInstructionIndex + _ySelector(registers);
                }

                return currentInstructionIndex + 1;
            }
        }

        private static Func<IDictionary<string, int>, int> AsNumberOrRegisterValue(string x)
        {
            return registers => int.TryParse(x, out var xAsNumber) ? xAsNumber : registers[x];
        }
    }
}
