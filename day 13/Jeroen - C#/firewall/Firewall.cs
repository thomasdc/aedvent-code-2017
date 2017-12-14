﻿using System.Collections.Generic;
using System.Linq;

namespace firewall
{
    class Firewall
    {
        public static int Severity(IList<(int depth, int range)> input) 
            => input.Select(x => Severity(x.depth, x.range, 0)).Sum();

        public static int DelayToEscape(IList<(int depth, int range)> input)
            => Enumerable.Range(0, int.MaxValue).First(delay => !input.Any(t => Caught(t.depth, t.range, delay)));

        static int Severity(int depth, int range, int delay) 
            => Caught(depth, range, delay) ? depth * range : 0;

        static bool Caught(int depth, int range, int delay) 
            => (depth + delay) % ((range - 1) * 2) == 0;
    }
}