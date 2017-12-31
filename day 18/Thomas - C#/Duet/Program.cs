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
            Console.WriteLine(Part2());
            Console.ReadLine();
        }

        private static long Part2()
        {
            var program0 = new Programma(0);
            program0.AssignInstructions(Parse(File.ReadAllLines("input.txt"), program0));
            var program1 = new Programma(1);
            program1.AssignInstructions(Parse(File.ReadAllLines("input.txt"), program1));
            program0.SetOtherProgram(program1);

            while (!(program0.BusyWaiting && program1.BusyWaiting))
            {
                program0.Tick();
                program1.Tick();
            }

            return program1.SendCount;
        }

        private static Instruction[] Parse(string[] inputLines, Programma program)
        {
            var instructions = new List<Instruction>();
            foreach (var line in inputLines)
            {
                var split = line.Split(' ');
                switch (split[0])
                {
                    case "snd": instructions.Add(new Send(split[1], program)); break;
                    case "set": instructions.Add(new Set(split[1], split[2])); break;
                    case "add": instructions.Add(new Add(split[1], split[2])); break;
                    case "mul": instructions.Add(new Multiply(split[1], split[2])); break;
                    case "mod": instructions.Add(new Mod(split[1], split[2])); break;
                    case "rcv": instructions.Add(new Receive(split[1], program)); break;
                    case "jgz": instructions.Add(new Jump(split[1], split[2])); break;
                }
            }

            return instructions.ToArray();
        }

        class Programma
        {
            public int Id;
            private Programma _otherProgram;
            private readonly Dictionary<string, long> _registers;
            private readonly Queue<long> _mailBox;
            public int SendCount;
            public bool BusyWaiting;
            private int _currentInstructionIndex;
            private Instruction[] _instructions;

            public Programma(int id)
            {
                Id = id;
                _mailBox = new Queue<long>();
                BusyWaiting = false;

                _registers = new Dictionary<string, long>();
                foreach (var @char in "abcdefghijklmnop".Select(_ => _.ToString()))
                {
                    _registers[@char] = 0;
                }

                _registers["p"] = id;
            }

            public void AssignInstructions(Instruction[] instructions)
            {
                _instructions = instructions;
            }

            public void Tick()
            {
                _currentInstructionIndex = _instructions[_currentInstructionIndex].Apply(_registers, _currentInstructionIndex);
            }

            public void Send(long value)
            {
                SendCount++;
                _otherProgram._mailBox.Enqueue(value);
            }

            public bool TryReceive(out long result)
            {
                if (_mailBox.TryDequeue(out result))
                {
                    BusyWaiting = false;
                    return true;
                }

                BusyWaiting = true;
                return false;
            }

            public void SetOtherProgram(Programma other)
            {
                _otherProgram = other;
                _otherProgram._otherProgram = this;
            }
        }

        public interface Instruction
        {
            int Apply(IDictionary<string, long> registers, int currentInstructionIndex);
        }

        class Send : Instruction
        {
            private readonly Func<IDictionary<string, long>, long> _xSelector;
            private readonly Programma _program;

            public Send(string x, Programma program)
            {
                _xSelector = AsNumberOrRegisterValue(x);
                _program = program;
            }

            public int Apply(IDictionary<string, long> registers, int currentInstructionIndex)
            {
                _program.Send(_xSelector(registers));
                return currentInstructionIndex + 1;
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

            public int Apply(IDictionary<string, long> registers, int currentInstructionIndex)
            {
                registers[_x] = _ySelector(registers);
                return currentInstructionIndex + 1;
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

            public int Apply(IDictionary<string, long> registers, int currentInstructionIndex)
            {
                registers[_x] += _ySelector(registers);
                return currentInstructionIndex + 1;
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

            public int Apply(IDictionary<string, long> registers, int currentInstructionIndex)
            {
                registers[_x] %= _ySelector(registers);
                return currentInstructionIndex + 1;
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

            public int Apply(IDictionary<string, long> registers, int currentInstructionIndex)
            {
                registers[_x] *= _ySelector(registers);
                return currentInstructionIndex + 1;
            }
        }

        class Receive : Instruction
        {
            private readonly string _x;
            private readonly Programma _program;

            public Receive(string x, Programma program)
            {
                _x = x;
                _program = program;
            }

            public int Apply(IDictionary<string, long> registers, int currentInstructionIndex)
            {
                if (_program.TryReceive(out var receivedValue))
                {
                    registers[_x] = receivedValue;
                    return currentInstructionIndex + 1;
                }

                return currentInstructionIndex;
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

            public int Apply(IDictionary<string, long> registers, int currentInstructionIndex)
            {
                if (_xSelector(registers) > 0)
                {
                    return (int) (currentInstructionIndex + _ySelector(registers));
                }

                return currentInstructionIndex + 1;
            }
        }

        private static Func<IDictionary<string, long>, long> AsNumberOrRegisterValue(string x)
        {
            return registers => long.TryParse(x, out var xAsNumber) ? xAsNumber : registers[x];
        }
    }
}
