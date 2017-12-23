using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace PermProm.Tests
{
    public class Tests
    {
        [Theory]
        [InlineData("abcde", 1, "eabcd")]
        [InlineData("deabc", 2, "bcdea")]
        [InlineData("bcdea", 4, "cdeab")]
        [InlineData("abcdmfghijklenop", 8, "ijklenopabcdmfgh")]
        [InlineData("baedc", 4, "aedcb")]
        public void ValidateSpin(string start, int spinSize, string expected)
        {
            // Arrange
            var spin = new Spin(spinSize);
            var positions = Program.FromChars(start);
            var indexOf = ToIndexOf(positions);
            
            // Act
            spin.Move(indexOf, positions);
            
            // Assert
            Assert.Equal(indexOf, ToIndexOf(positions));
            Assert.Equal(expected, Program.ToChars(positions));
            
        }

        [Theory]
        [InlineData("dcbae", 4, 1, "debac")]
        [InlineData("bcdea", 2, 3, "bceda")]
        [InlineData("ijklenopabcdfmgh", 5, 12, "ijklefopabcdnmgh")]
        public void ValidateExchange(string start, int positionA, int positionB, string expected)
        {
            // Arrange
            var exchange = new Exchange(positionA, positionB);
            var positions = Program.FromChars(start);
            var indexOf = ToIndexOf(positions);

            // Act
            exchange.Move(indexOf, positions);

            // Assert
            Assert.Equal(expected, Program.ToChars(positions));
            Assert.Equal(indexOf, ToIndexOf(positions));
        }
        
        [Theory]
        [InlineData("dcbae", "d", "a", "acbde")]
        [InlineData("cdbae", "b", "c", "bdcae")]
        public void ValidatePartner(string start, string program1, string program2, string expected)
        {
            // Arrange
            var partner = new Partner(program1, program2);
            var positions = Program.FromChars(start);
            var indexOf = ToIndexOf(positions);

            // Act
            partner.Move(indexOf, positions);

            // Assert
            Assert.Equal(expected, Program.ToChars(positions));
            Assert.Equal(indexOf, ToIndexOf(positions));
        }

        [Fact]
        public void ValidateDance()
        {
            // Arrange
            var moves = new IDanceMove[] {new Spin(1), new Exchange(3, 4), new Partner("e", "b"), new Spin(4)};
            var indexOf = Enumerable.Range(0, 5).ToArray();
            var positions = Enumerable.Range(0, 5).ToArray();
            
            // Act
            foreach (var move in moves)
            {
                move.Move(indexOf, positions);
                Debug.WriteLine(Program.ToChars(positions));
            }

            // Assert
            Assert.Equal("aedcb", Program.ToChars(positions));
        }

        [Fact]
        public void ValidateIndexOf()
        {
            Assert.Equal(new[] {0, 4, 3, 2, 1}, ToIndexOf(new[] {0, 4, 3, 2, 1}));
            Assert.Equal(new[] {4, 0, 1, 2, 3}, ToIndexOf(new[] {1, 2, 3, 4, 0}));
        }

        private int[] ToIndexOf(int[] positions)
        {
            return Enumerable.Range(0, positions.Length).Select(_ => Array.IndexOf(positions, _)).ToArray();
        }
    }
}