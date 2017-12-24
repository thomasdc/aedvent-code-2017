using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using Xunit;
using static System.Math;

class Program
{
    static void Main(string[] args)
    {
        var program = (
            from line in File.ReadLines("input.txt")
            let s = line.Split()
            select (instruction: s[0], arg1: s[1], arg2: s[2])
        ).ToList().AsReadOnly();

        Run(() =>
        {
            var memory = "abcdefgh".ToDictionary(c => c, c => 0);
            return Evaluate(memory, program, null);
        });
        Run(() =>
        {
            var memory = "abcdefgh".ToDictionary(_ => _, _ => 0);
            Evaluate(memory, program, null);

            int a = 0, b = 0, c = 0, d = 0, e = 0, f = 0, g = 0, h = 0;

            a = 0;
            //  0 set b 99
            b = 99;
            //  1 set c b
            c = b;
            //  2 jnz a 2 => if a != 0 goto 4
            //  3 jnz 1 5 => else goto 8
            if (a != 0)
            {
                //  4 mul b 100
                b *= 100;
                //  5 sub b -100000
                b -= -100000;
                //  6 set c b
                c = b;
                //  7 sub c -17000
                c -= -17000;
            }
            //  8 set f 1
            do
            {
                f = 1;
                //  9 set d 2
                d = 2;
                do
                {
                    // 10 set e 2
                    e = 2;
                    do
                    {
                        // 11 set g d
                        // 12 mul g e
                        // 13 sub g b
                        g = d * e - b;
                        // 14 jnz g 2
                        if (g == 0)
                        {
                            // 15 set f 0;
                            f = 0;
                        }
                        // 16 sub e -1
                        e++;
                        // 17 set g e
                        // 18 sub g b
                        g = e - b;
                        // 19 jnz g -8
                    } while (g != 0);
                    // 20 sub d -1
                    d++;
                    // 21 set g d
                    // 22 sub g b
                    g = d - b;
                    // 23 jnz g -13 # => goto 10
                } while (g != 0);

                // 24 jnz f 2 => if f != 0 goto 26
                if (f == 0)
                {
                    // 25 sub h -1
                    h++;
                }
                // 26 set g b
                // 27 sub g c
                g = b - c;

                // 28 jnz g 2 => if g != 0 goto 30
                if (g == 0)
                {
                    // 29 jnz 1 3
                    break;
                }

                // 30 sub b -17
                b -= -17;
                // 31 jnz 1 -23 goto 8;
            } while (true);
            Assert.Equal(a, memory['a']);
            Assert.Equal(b, memory['b']);
            Assert.Equal(c, memory['c']);
            Assert.Equal(d, memory['d']);
            Assert.Equal(e, memory['e']);
            Assert.Equal(f, memory['f']);
            Assert.Equal(g, memory['g']);
            Assert.Equal(h, memory['h']);
            return h;
        });
        Run(() =>
        {
            var memory = "abcdefgh".ToDictionary(_ => _, _ => 0);
            Evaluate(memory, program, null);

            int a = 0, b = 99, c = b, d = 0, e = 0, f = 0, g = 0, h = 0;

            if (a != 0)
            {
                b = b * 100 + 100000;
                c = b + 17000;
            }

            do
            {
                f = 1;
                d = 2;
                do
                {
                    e = 2;
                    do
                    {
                        if (d * e == b)
                        {
                            f = 0;
                            e = b;
                        }
                        else
                        {
                            e++;
                        }
                    } while (e != b);

                    if (f == 0)
                    {
                        d = b;
                    }
                    else
                    {
                        d++;
                    }
                } while (d != b);

                if (f == 0)
                {
                    h++;
                }

                if (b == c)
                {
                    break;
                }
                b += 17;
            } while (true);

            Assert.Equal(a, memory['a']);
            Assert.Equal(b, memory['b']);
            Assert.Equal(c, memory['c']);
            Assert.Equal(d, memory['d']);
            Assert.Equal(e, memory['e']);
            Assert.Equal(f, memory['f']);
            Assert.Equal(g, memory['g']);
            Assert.Equal(h, memory['h']);
            return h;
        });
        Run(() =>
        {
            bool prime(int n)
            {
                    if (n == 1) return false;
                    if (n == 2) return true;
                    if (n % 2 == 0) return false;
                    var limit = (int)Ceiling(Sqrt(n));
                    for (var i = 3; i <= limit; i += 2)
                    {
                        if (n % i == 0) return false;
                    }
                    return true;
            }
            //b = 109900;
            //c = 126900;
            var h = 0;
            for (var b = 109900; b <= 126900; b += 17)
            {
                if (!prime(b))
                {
                    h++;
                }
            }


            return h;
        });
    }

    private static int Evaluate(IDictionary<char, int> memory, IReadOnlyList<(string instruction, string arg1, string arg2)> program, StreamWriter writer)
    {
        char Register(string s) => s[0];
        int Value(IDictionary<char, int> mem, string s) => char.IsLetter(s[0]) ? mem[s[0]] : int.Parse(s);
        int i = 0;
        int nofmultiplications = 0;
        while (i >= 0 && i < program.Count)
        {
            var instruction = program[i];
            if (writer != null)
            {
                var representation = $"{i:D5} {instruction.instruction} {instruction.arg1} {instruction.arg2}".PadRight(18);
                writer.Write(representation);
            }
            switch (instruction.instruction)
            {
                case "set":
                    {
                        memory[Register(instruction.arg1)] = Value(memory, instruction.arg2);
                        i++;
                        break;
                    }
                case "sub":
                    {
                        memory[Register(instruction.arg1)] -= Value(memory, instruction.arg2);
                        i++;
                        break;
                    }
                case "mul":
                    {
                        nofmultiplications++;
                        memory[Register(instruction.arg1)] *= Value(memory, instruction.arg2);
                        i++;
                        break;
                    }
                case "jnz":
                    {
                        if (char.IsDigit(instruction.arg1[0]) || memory[Register(instruction.arg1)] != 0)
                            i += Value(memory, instruction.arg2);
                        else
                            i++;
                        break;
                    }
            }
            if (writer != null)
            {
                writer.WriteLine($"- {string.Join("|", memory.Values.Select(x => x.ToString("D8")))}");
            }
        }
        return nofmultiplications;
    }

    static void Run<T>(Func<T> f)
    {
        var sw = Stopwatch.StartNew();
        var result = f();
        Console.WriteLine($"{result} - {sw.Elapsed}");
    }
}
