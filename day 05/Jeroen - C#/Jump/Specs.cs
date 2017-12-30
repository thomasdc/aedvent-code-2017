﻿using Xunit;

namespace Jump
{
    public class Specs
    {
        [Theory]
        [InlineData(5, 0, 3, 0, 1, -3)]
        public void Test1(int expected, params int[] input)
        {
            var jumps = Jumps.CalculateJumps(input, _ => 1);
            Assert.Equal(expected, jumps);
        }
        [Theory]
        [InlineData(10, 0, 3, 0, 1, -3)]
        public void Test2(int expected, params int[] input)
        {
            var jumps = Jumps.CalculateJumps(input, v => v >= 3 ? -1 : 1);
            Assert.Equal(expected, jumps);
        }

    }
}