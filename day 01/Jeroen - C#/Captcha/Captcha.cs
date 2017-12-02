using System;
using System.Collections.Generic;
using System.Linq;

class Captcha
{
    public static int Calculate(string input, int lookahead)
    {
        return GetCandidates(input, lookahead).Sum();
    }

    private static IEnumerable<int> GetCandidates(string input, int lookahead)
    {
        var enumerator1 = WrapAround(input, lookahead).GetEnumerator();
        var enumerator2 = WrapAround(input, lookahead).Skip(lookahead).GetEnumerator();
        if (!enumerator1.MoveNext()) yield break;
        if (!enumerator2.MoveNext()) yield break;
        do
        {
            int first = enumerator1.Current;
            int second = enumerator2.Current;
            if (first == second) yield return first - '0';
        } while (enumerator1.MoveNext() && enumerator2.MoveNext());


    }

    static IEnumerable<char> WrapAround(string input, int n)
    {
        for (int i = 0; i < input.Length; i++)
            yield return input[i];
        for (int i = 0; i < input.Length && i < n; i++)
            yield return input[i];
    }
}