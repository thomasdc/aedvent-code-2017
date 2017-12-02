using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        var input = File.ReadAllText("input.txt");

        var sw = Stopwatch.StartNew();
        var result1 = Captcha.Calculate(input, 1);
        var result2 = Captcha.Calculate(input, input.Length / 2);
        var elapsed = sw.Elapsed;

        Console.WriteLine(result1);
        Console.WriteLine(result2);
        Console.WriteLine(elapsed);
    }
}