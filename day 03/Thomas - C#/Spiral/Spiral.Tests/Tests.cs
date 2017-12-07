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
    }
}
