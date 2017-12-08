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
        [InlineData(1, 2)]
        [InlineData(5, 10)]
        [InlineData(26, 54)]
        [InlineData(655, 747)]
        public void TestPart2(int input, int expected)
        {
            var actual = Program.Part2(input);
            Assert.Equal(expected, actual);
        }
    }
}
