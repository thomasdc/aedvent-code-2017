using System.Collections.Generic;
using System.Linq;

namespace generator
{
    public static class Generator
    {
        public static int GetNofMatches(int seedA, int seedB, int take, int multipleOfA = 1, int multipleOfB = 1) 
            => A(seedA, multipleOfA).Zip(B(seedB, multipleOfB), (a, b) => ((a: a, b: b)))
                .Take(take)
                .Count(x => (x.a & 0xFFFFL) == (x.b & 0xFFFFL));

        public static IEnumerable<long> A(long seed, int multipleOf = 1) => Sequence(seed, 16807, 2147483647).Where(i => i % multipleOf == 0);
        public static IEnumerable<long> B(long seed, int multipleOf = 1) => Sequence(seed, 48271, 2147483647).Where(i => i % multipleOf == 0);
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