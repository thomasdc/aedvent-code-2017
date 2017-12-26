using System;
using System.Collections.Generic;

namespace Spinlock
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part2(359));
        }

        private static int Part2(int stepsize)
        {
            var list = new LinkedList<int>();
            list.AddFirst(0);
            var currentNode = list.First; 
            
            for (var i = 1; i <= 50_000_000; i++)
            {
                if (i % 100_000 == 0) Console.WriteLine(i);
                for (var j = 0; j < stepsize; j++)
                {
                    currentNode = currentNode.Next ?? list.First;
                }

                currentNode = list.AddAfter(currentNode, i);
            }

            return list.Find(0).Next.Value;
        }
    }
}
