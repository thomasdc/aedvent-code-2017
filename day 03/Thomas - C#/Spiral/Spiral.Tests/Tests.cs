using Xunit;

namespace Spiral.Tests
{
    public class Tests
    {
        [Theory]
        [InlineData(1, 0)]
        [InlineData(12, 3)]
        [InlineData(23, 2)]
        [InlineData(1024, 31)]
        public void TestPart1(int input, int expected)
        {
            var actual = Program.Part1(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0, 1, 1, 1, 1)]
        [InlineData(1, 1, 1, 1, 1, 0)]
        [InlineData(1, -1, 1, 2, -1, 2)]
        [InlineData(2, 2, -1, 2, 2, -2)]
        [InlineData(0, 0, 0, 1, 0, 1)]
        public void TestNext(int depth, int row, int column, int expectedNextDepth, int expectedNextRow, int expectedNextColumn)
        {
            var result = Program.Next(depth, row, column);
            Assert.Equal(expectedNextDepth, result.nextDepth);
            Assert.Equal(expectedNextRow, result.nextRow);
            Assert.Equal(expectedNextColumn, result.nextColumn);
        }
    }
}
