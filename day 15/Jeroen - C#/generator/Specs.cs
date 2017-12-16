﻿using System.Linq;
using Xunit;

namespace generator
{
    public class Specs
    {
        private const int SeedA = 65;
        private const int SeedB = 8921;

        [Fact]
        public void TestGeneratorA()
        {
            var result = Generator.A(SeedA).Take(5).ToArray();
            Assert.Equal(new[] { 1092455L, 1181022009, 245556042, 1744312007, 1352636452 }, result);
        }

        [Fact]
        public void TestGeneratorB()
        {
            var result = Generator.B(SeedB).Take(5).ToArray();
            Assert.Equal(new[] { 430625591L, 1233683848, 1431495498, 137874439, 285222916 }, result);
        }

        [Fact]
        public void TestSequence()
        {
            var sequence = Generator.A(SeedA).Zip(Generator.B(SeedB), (a, b) => (a: a, b: b)).Take(5).ToList();
            var x = sequence[2];
            Assert.True((x.a & 0xFFFFL) == (x.b & 0xFFFFL));
        }

        [Fact]
        public void TestNofMatchesPart1()
        {
            Assert.Equal(588, Generator.GetNofMatches(SeedA,SeedB,40_000_000));
        }
        [Fact]
        public void TestNofMatchesPart2()
        {
            Assert.Equal(309, Generator.GetNofMatches(SeedA, SeedB, 5_000_000, 4, 8));
        }


    }
}