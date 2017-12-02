using System.Collections.Generic;
using System.IO;
using System.Linq;

static class CheckSum
{
    public static int CheckSum1(TextReader reader) => ReadInts(reader)
        .Select(LargestDifference)
        .Sum();

    public static int CheckSum2(TextReader reader) => ReadInts(reader)
        .Select(EvenDivision)
        .Sum();

    static int LargestDifference(IEnumerable<int> input)
    {
        var aggregate = input.Aggregate(
            (min: int.MaxValue, max: int.MinValue),
            (x, n) => (n < x.min ? n : x.min, n > x.max ? n : x.max)
        );
        return aggregate.max - aggregate.min;
    }
    static int EvenDivision(IEnumerable<int> input)
    {
        var ints = input.OrderByDescending(i => i).ToArray();
        return ( 
            from x in ints.Select((value, index) => (idx: index, value: value))
            from y in ints.Skip(x.idx + 1)
            where (decimal) x.value % y == 0
            select x.value / y
            ).FirstOrDefault();
    }

    public static IEnumerable<IEnumerable<int>> ReadInts(TextReader reader) 
        => ReadLines(reader).Select(line => line.Split('\t').Select(int.Parse));

    private static IEnumerable<string> ReadLines(TextReader reader)
    {
        while (reader.Peek() >= 0) yield return reader.ReadLine();
    }
}