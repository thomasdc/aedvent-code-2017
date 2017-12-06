using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Memory
{
    static class Memory
    {
        public static (int steps, int loopSize) Cycles(ImmutableArray<byte> input)
        {
            var list = new List<ImmutableArray<byte>>();
            int steps = 0;
            int max = 0;
            var cycle = input;
            while (true)
            {
                var m = cycle.FindMax();
                if (m.value > max) max = m.value;
                steps++;
                cycle = DoOneCycle(cycle);

                var index = list.FindIndex(x => x.SequenceEqual(cycle));
                if (index >= 0)
                {
                    Console.WriteLine(max);
                    return (steps, list.Count - index);
                }

                list.Add(cycle);
            };

        }
        public static (int steps, int loopSize) MutableCycles(ImmutableArray<byte> input)
        {
            var list = new List<ImmutableArray<byte>>();
            int steps = 0;
            var cycle = input;
            int max = 0;
            while (true)
            {
                var m = cycle.FindMax();
                if (m.value > max) max = m.value;
                steps++;
                cycle = DoOneCycle(cycle);
                var index = list.FindIndex(x => x.SequenceEqual(cycle));
                if (index >= 0)
                {
                    Console.WriteLine(max);
                    return (steps, list.Count - index);
                }

                list.Add(cycle);
            };
        }

        public static (int index, int value) FindMax(this IEnumerable<byte> input) =>
            input
                .Select((value, index) => (index: index, value: value))
                .Aggregate((index: -1, value: int.MinValue), (x, y) => y.value > x.value ? y : x);

        static void DoOneCycle(byte[] input)
        {
            var max = input.FindMax();
            var length = input.Length;
            var quotient = max.value / length;
            var remainder = max.value % length;
            for (int i = 0; i < input.Length; i++)
            {
                var value = input[i];
                var j = (i + length - max.index - 1) % length;
                var term1 = i == max.index ? 0 : value;
                var term2 = quotient;
                var term3 = j < remainder ? 1 : 0;
                input[i] = (byte)(term1 + term2 + term3);
            }
        }
        public static ImmutableArray<byte> DoOneCycle(ImmutableArray<byte> input)
        {
            var max = input.FindMax();
            var length = input.Length;
            var quotient = max.value / length;
            var remainder = max.value % length;

            // 1 2 6 3  
            // 3 3 1 5  
            // max = (2,6)
            // length = 4
            // quotient = 1
            // remainder = 2

            // 0 1 2 3  i
            // F F T F  i == max.index ?
            //*1 2 0 3  0 : value
            //*1 1 1 1  
            // 1 2 3 0  j = (i + length - max.index - 1) % length
            // T F F T  j < remainder ? 
            //*1 0 0 1  
            //*3 3 1 5


            var q =
                from t in input.Select((value, i) => (value: value, i: i))
                let i = t.i
                let value = t.value
                let j = (i + length - max.index - 1) % length
                let term1 = i == max.index ? 0 : value
                let term2 = quotient
                let term3 = j < remainder ? 1 : 0
                select (term1, term2, term3);

            var redistributed = q.Select(x => (byte)(x.Item1 + x.Item2 + x.Item3)).ToImmutableArray();
            return redistributed;
        }


    }
}