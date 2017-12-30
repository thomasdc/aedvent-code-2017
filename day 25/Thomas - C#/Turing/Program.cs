using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Turing
{
    class Program
    {
        static void Main(string[] args)
        {
            var turingProgram = Parse(File.ReadAllLines("input.txt"));
            Console.WriteLine(Part1(turingProgram));
            Console.ReadLine();
        }

        private static int Part1((IDictionary<string, State> states, string initialState, int numberOfSteps) turingProgram)
        {
            var tape = Run(turingProgram);
            return tape.Count(_ => _);
        }

        private static LinkedList<bool> Run((IDictionary<string, State> states, string initialState, int numberOfSteps) program)
        {
            var tape = new LinkedList<bool>();
            tape.AddFirst(false);
            var currentPosition = tape.First;
            var currentState = program.states[program.initialState];

            for (var stepIndex = 0; stepIndex < program.numberOfSteps; stepIndex++)
            {
                var instruction = currentState.GetInstruction(currentPosition.Value);
                currentPosition.Value = instruction.ValueToWrite;
                currentPosition = instruction.MoveToTheRight
                    ? (currentPosition.Next ?? tape.AddAfter(currentPosition, false))
                    : (currentPosition.Previous ?? tape.AddBefore(currentPosition, false));
                currentState = program.states[instruction.NextState];
            }

            return tape;
        }

        private static (IDictionary<string, State> states, string initialState, int numberOfSteps) Parse(string[] input)
        {
            var initialState = input[0].Trim('.').Split(' ').Last();
            var numberOfSteps = int.Parse(input[1].Trim('.').Split(' ').Reverse().Skip(1).First());

            var numberOfStates = (input.Length - 2) / 10;
            var states = new Dictionary<string, State>();
            foreach (var stateIndex in Enumerable.Range(0, numberOfStates))
            {
                var lineIndexOffset = 2 + stateIndex * 10;
                var stateName = input[1 + lineIndexOffset].Trim(':').Split(' ').Last();
                var instruction0 = ParseInstruction(input[3 + lineIndexOffset], input[4 + lineIndexOffset], input[5 + lineIndexOffset]);
                var instruction1 = ParseInstruction(input[7 + lineIndexOffset], input[8 + lineIndexOffset], input[9 + lineIndexOffset]);
                states[stateName] = new State(instruction0, instruction1);
            }

            return (states, initialState, numberOfSteps);
        }

        private static Instruction ParseInstruction(string line1, string line2, string line3)
        {
            var valueToWrite = line1.Trim().Trim('.', '-').Split(' ').Last() == "1";
            var moveToTheRight = line2.Trim().Trim('.', '-').Split(' ').Last() == "right";
            var nextState = line3.Trim().Trim('.', '-').Split(' ').Last();
            return new Instruction(valueToWrite, moveToTheRight, nextState);
        }

        struct State
        {
            private Instruction Instruction0 { get; }
            private Instruction Instruction1 { get; }

            public State(Instruction instruction0, Instruction instruction1)
            {
                Instruction0 = instruction0;
                Instruction1 = instruction1;
            }

            public Instruction GetInstruction(bool value)
            {
                return value ? Instruction1 : Instruction0;
            }
        }

        struct Instruction
        {
            public bool ValueToWrite { get; }
            public bool MoveToTheRight { get; }
            public string NextState { get; }

            public Instruction(bool valueToWrite, bool moveToTheRight, string nextState)
            {
                ValueToWrite = valueToWrite;
                MoveToTheRight = moveToTheRight;
                NextState = nextState;
            }
        }
    }
}
