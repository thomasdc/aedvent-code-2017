using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Passphrase
{
    class Program
    {
        static void Main(string[] args)
        {
            var valid1 = ReadLines(new StreamReader("input.txt"))
                .Count(IsValidPassword1);
            Console.WriteLine(valid1);

            var valid2 = ReadLines(new StreamReader("input.txt"))
                .Count(IsValidPassword2);
            Console.WriteLine(valid2);
        }

        private static bool IsValidPassword1(string line)
        {
            var words = line.Split(' ');
            return words.Length == words.Distinct().Count();
        }
        private static bool IsValidPassword2(string line)
        {
            var words = line.Split(' ').Select(w => new string(w.OrderBy(c => c).ToArray())).ToArray();
            return words.Length == words.Distinct().Count();
        }

        static IEnumerable<string> ReadLines(TextReader reader)
        {
            while (reader.Peek() >= 0) yield return reader.ReadLine();
        }
    }
}
