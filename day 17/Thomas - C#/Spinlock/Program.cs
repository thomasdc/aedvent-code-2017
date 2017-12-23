using System;
using System.Collections.Generic;

namespace Spinlock
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part1(359));
        }

        private static int Part1(int stepsize)
        {
            var list = new List<int> {0};
            var location = 0;
            
            for (var i = 1; i <= 2017; i++)
            {
                location = (location + stepsize) % list.Count + 1;
                list.Insert(location, i);
            }

            return list[list.IndexOf(2017) + 1];
        }
    }
}
