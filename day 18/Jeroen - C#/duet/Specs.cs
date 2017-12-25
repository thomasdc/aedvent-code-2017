using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

public class Specs
{
    [Fact]
    public void TestProgram()
    {
        var program = "set a 1\r\n" +
                      "add a 2\r\n" +
                      "mul a a\r\n" +
                      "mod a 5\r\n" +
                      "snd a\r\n" +
                      "set a 0\r\n" +
                      "rcv a\r\n" +
                      "jgz a -1\r\n" +
                      "set a 1\r\n" +
                      "jgz a -2";
        var cpu = new CPU1();
        cpu.Run(program.ReadLines().ToArray());
        Assert.Equal(4, cpu.LastPlayed);
    }
}

public static class Extensions
{
    public static IEnumerable<string> ReadLines(this string s)
    {
        using (var reader = new StringReader(s))
        {
            while (reader.Peek() >= 0) yield return reader.ReadLine();
        }
    }
}