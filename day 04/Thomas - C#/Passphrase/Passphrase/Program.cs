using System;
using System.IO;
using System.Linq;

namespace Passphrase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part2(File.ReadAllLines("input.txt")));
        }

        public static int Part1(string[] input)
        {
            return input.Select(row => row.Split(' ')).Count(words => words.Length == words.Distinct().Count());
        }
        
        public static int Part2(string[] input)
        {
            return input.Select(row => row.Split(' ')).Select(words => words.Select(SortChars)).Count(words => words.Count() == words.Distinct().Count());
        }

        private static string SortChars(string word)
        {
            return new string(word.OrderBy(@char => @char).ToArray());
        }
    }
}
