using Xunit;

namespace Checksum.Tests
{
    public class Tests
    {
        [Theory]
        [InlineData("5\t1\t9\t5\r7\t5\t3\r2\t4\t6\t8", 18)]
        public void TestPart1(string input, int expected)
        {
            var actual = Program.Part1(input);
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("5\t9\t2\t8\r9\t4\t7\t3\r3\t8\t6\t5", 9)]
        public void TestPart2(string input, int expected)
        {
            var actual = Program.Part2(input);
            Assert.Equal(expected, actual);
        }
    }
}