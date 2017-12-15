﻿using System.Collections.Generic;
using System.Linq;

namespace generator
{
    public static class Generator
    {
        private const int FactorA = 16807;
        private const int FactorB = 48271;
        private const int Divisor = 2147483647;

        public static int GetNofMatches(int seedA, int seedB, int take, int multipleOfA = 1, int multipleOfB = 1) 
            => A(seedA, multipleOfA).Zip(B(seedB, multipleOfB), (a, b) => ((a: a, b: b)))
                .Take(take)
                .Count(x => (x.a & 0xFFFFL) == (x.b & 0xFFFFL));

        public static IEnumerable<long> A(long seed, int multipleOf = 1) => Sequence(seed, FactorA, Divisor).Where(i => i % multipleOf == 0);
        public static IEnumerable<long> B(long seed, int multipleOf = 1) => Sequence(seed, FactorB, Divisor).Where(i => i % multipleOf == 0);
        public static IEnumerable<long> Sequence(long input, long factor, long divisor)
        {
            var i = input;
            while (true)
            {
                i = i * factor % divisor;
                yield return i;
            }
        }
    }
}