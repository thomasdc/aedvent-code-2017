using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        using (var reader = new StreamReader(File.OpenRead("input.txt")))
        {
            Console.WriteLine(CheckSum.CheckSum1(reader));
        }
        using (var reader = new StreamReader(File.OpenRead("input.txt")))
        {
            Console.WriteLine(CheckSum.CheckSum2(reader));
        }
    }
}
