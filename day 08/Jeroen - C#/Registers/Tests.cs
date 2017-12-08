using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Xunit;

namespace Registers
{

    struct Instruction
    {
        internal static Regex r = new Regex(
            @"^(?<register>[a-z]+) (?<operation>inc|dec) (?<amount>-{0,1}\d+) if (?<testregister>[a-z]+) (?<operator>[<>=!]{1,2}) (?<comparisonValue>-{0,1}[\d]+)", RegexOptions.Compiled);

        public static Instruction Parse(string input)
        {
            var match = r.Match(input);
            return new Instruction(
                match.Groups["register"].Value,
                ParseOperation(match.Groups["register"].Value, match.Groups["operation"].Value, int.Parse(match.Groups["amount"].Value)),
                ParsePredicate(match.Groups["testregister"].Value, match.Groups["operator"].Value, match.Groups["comparisonValue"].Value)
                );
        }

        private static Func<IDictionary<string,int>, bool> ParsePredicate(string s, string comparisonOperator, string value)
        {
            int i = int.Parse(value);
            switch (comparisonOperator)
            {
                case "<":
                    return d => d[s] < i;
                case "<=":
                    return d => d[s] <= i;
                case ">":
                    return d => d[s] > i;
                case ">=":
                    return d => d[s] >= i;
                case "==":
                    return d => d[s] == i;
                case "!=":
                    return d => d[s] != i;
            }
            throw new ArgumentException(nameof(comparisonOperator));
        }

        private static Action<IDictionary<string, int>> ParseOperation(string s, string operation, int amount)
        {
            switch (operation)
            {
                case "dec": return d => d[s] -= amount;
                case "inc": return d => d[s] += amount;
            }
            throw new ArgumentException();
        }

        public readonly string Register;
        public readonly Action<IDictionary<string,int>> Action;
        public readonly Func<IDictionary<string, int>, bool> Predicate;
        public Instruction(string register, Action<IDictionary<string,int>> action, Func<IDictionary<string, int>, bool> predicate)
        {
            Register = register;
            Action = action;
            Predicate = predicate;
        }

        public int Apply(IDictionary<string, int> dictionary)
        {
            if (Predicate(dictionary))
                Action(dictionary);
            return dictionary[Register];
        }
    }

    public class Tests
    {
        // if (?<testregister>) (?<comparison>[<>=!]{1,2}) (?<comparisonValue>)
        [Theory]
        [InlineData("b inc 5 if a > 1")]
        public void TestRegex(string input)
        {
            var result = Instruction.r.Match(input);
            Assert.True(result.Success);
            Assert.Equal("b", result.Groups["register"].Value);
            Assert.Equal("inc", result.Groups["operation"].Value);
            Assert.Equal("5", result.Groups["amount"].Value);
            Assert.Equal("a", result.Groups["testregister"].Value);
            Assert.Equal(">", result.Groups["operator"].Value);
            Assert.Equal("1", result.Groups["comparisonValue"].Value);
        }

        [Fact]
        public void TestInstructionParse()
        {
            var input = "abc dec -25 if def > -5";
            var instruction = Instruction.Parse(input);

            Assert.Equal("abc", instruction.Register);

            var dictionary = new Dictionary<string, int> { ["abc"] = 0, ["def"] = -5};

            instruction.Apply(dictionary);
            Assert.Equal(0, dictionary["abc"]);

            dictionary["def"] = -4;
            instruction.Apply(dictionary);
            Assert.Equal(25, dictionary["abc"]);
        }


        [Fact]
        public void Test()
        {

            var sample = @"b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10";

            var instructions = ReadLines(sample).Select(Instruction.Parse).ToList();

            var registers = instructions.Select(i => i.Register).Distinct().ToDictionary(s => s, s => 0);

            foreach (var instruction in instructions)
                instruction.Apply(registers);
            Assert.Equal(1, registers.Values.Max());

        }

        private IEnumerable<string> ReadLines(string s)
        {
            using (var reader = new StringReader(s))
            {
                while (reader.Peek() >= 0)
                    yield return reader.ReadLine();
            }
        }
    }
}