using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Duet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part1());
            Console.ReadLine();
        }

        private static long Part1()
        {
            var instructions = Parse(File.ReadAllLines("input.txt"));
            var registers = new Dictionary<string, long>();
            foreach (var @char in "abcdefghijklmnop".Select(_ => _.ToString()))
            {
                registers[@char] = 0;
            }

            var currentInstructionIndex = 0;
            var @continue = true;
            while (@continue)
            {
                var applied = instructions[currentInstructionIndex].Apply(registers, currentInstructionIndex);
                currentInstructionIndex = applied.nextInstructionIndex;
                @continue = applied.@continue;
            }

            return Sound.LastFrequencyPlayed;
        }

        private static Instruction[] Parse(string[] inputLines)
        {
            var instructions = new List<Instruction>();
            foreach (var line in inputLines)
            {
                var split = line.Split(' ');
                switch (split[0])
                {
                    case "snd": instructions.Add(new Sound(split[1])); break;
                    case "set": instructions.Add(new Set(split[1], split[2])); break;
                    case "add": instructions.Add(new Add(split[1], split[2])); break;
                    case "mul": instructions.Add(new Multiply(split[1], split[2])); break;
                    case "mod": instructions.Add(new Mod(split[1], split[2])); break;
                    case "rcv": instructions.Add(new Recover(split[1])); break;
                    case "jgz": instructions.Add(new Jump(split[1], split[2])); break;
                }
            }

            return instructions.ToArray();
        }

        public interface Instruction
        {
            (int nextInstructionIndex, bool @continue) Apply(IDictionary<string, long> registers, int currentInstructionIndex);
        }

        class Sound : Instruction
        {
            private readonly string _x;
            public static long LastFrequencyPlayed;

            public Sound(string x)
            {
                _x = x;
            }

            public (int nextInstructionIndex, bool @continue) Apply(IDictionary<string, long> registers, int currentInstructionIndex)
            {
                LastFrequencyPlayed = registers[_x];
                return (currentInstructionIndex + 1, true);
            }
        }

        class Set : Instruction
        {
            private readonly string _x;
            private readonly Func<IDictionary<string, long>, long> _ySelector;

            public Set(string x, string y)
            {
                _x = x;
                _ySelector = AsNumberOrRegisterValue(y);
            }

            public (int nextInstructionIndex, bool @continue) Apply(IDictionary<string, long> registers, int currentInstructionIndex)
            {
                registers[_x] = _ySelector(registers);
                return (currentInstructionIndex + 1, true);
            }
        }

        class Add : Instruction
        {
            private readonly string _x;
            private readonly Func<IDictionary<string, long>, long> _ySelector;

            public Add(string x, string y)
            {
                _x = x;
                _ySelector = AsNumberOrRegisterValue(y);
            }

            public (int nextInstructionIndex, bool @continue) Apply(IDictionary<string, long> registers, int currentInstructionIndex)
            {
                registers[_x] += _ySelector(registers);
                return (currentInstructionIndex + 1, true);
            }
        }

        class Mod : Instruction
        {
            private readonly string _x;
            private readonly Func<IDictionary<string, long>, long> _ySelector;

            public Mod(string x, string y)
            {
                _x = x;
                _ySelector = AsNumberOrRegisterValue(y);
            }

            public (int nextInstructionIndex, bool @continue) Apply(IDictionary<string, long> registers, int currentInstructionIndex)
            {
                registers[_x] %= _ySelector(registers);
                return (currentInstructionIndex + 1, true);
            }
        }

        class Multiply : Instruction
        {
            private readonly string _x;
            private readonly Func<IDictionary<string, long>, long> _ySelector;

            public Multiply(string x, string y)
            {
                _x = x;
                _ySelector = AsNumberOrRegisterValue(y);
            }

            public (int nextInstructionIndex, bool @continue) Apply(IDictionary<string, long> registers, int currentInstructionIndex)
            {
                registers[_x] *= _ySelector(registers);
                return (currentInstructionIndex + 1, true);
            }
        }

        class Recover : Instruction
        {
            private readonly string _x;

            public Recover(string x)
            {
                _x = x;
            }

            public (int nextInstructionIndex, bool @continue) Apply(IDictionary<string, long> registers, int currentInstructionIndex)
            {
                return (currentInstructionIndex, false);
            }
        }

        class Jump : Instruction
        {
            private readonly Func<IDictionary<string, long>, long> _xSelector;
            private readonly Func<IDictionary<string, long>, long> _ySelector;

            public Jump(string x, string y)
            {
                _xSelector = AsNumberOrRegisterValue(x);
                _ySelector = AsNumberOrRegisterValue(y);
            }

            public (int nextInstructionIndex, bool @continue) Apply(IDictionary<string, long> registers, int currentInstructionIndex)
            {
                if (_xSelector(registers) > 0)
                {
                    return ((int) (currentInstructionIndex + _ySelector(registers)), true);
                }

                return (currentInstructionIndex + 1, true);
            }
        }

        private static Func<IDictionary<string, long>, long> AsNumberOrRegisterValue(string x)
        {
            return registers => long.TryParse(x, out var xAsNumber) ? xAsNumber : registers[x];
        }
    }
}
