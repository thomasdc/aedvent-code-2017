using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiralMemory {
    class Program {
        static void Main(string[] args) {
            Day3.PrintExercises();
        }
    }

    public class Day3
    {
        public static void PrintExercises()
        {
            // Examples
            Console.Out.WriteLine(new Day3().CalculateNumSteps(1) == 0);
            Console.Out.WriteLine(new Day3().CalculateNumSteps(11) == 2);
            Console.Out.WriteLine(new Day3().CalculateNumSteps(12) == 3);
            Console.Out.WriteLine(new Day3().CalculateNumSteps(17) == 4);
            Console.Out.WriteLine(new Day3().CalculateNumSteps(23) == 2);
            Console.Out.WriteLine(new Day3().CalculateNumSteps(1024) == 31);

            // Real input
            Console.Out.WriteLine(new Day3().CalculateNumSteps(325489));
        }

        public int CalculateNumSteps(int memoryLocation)
        {
            if (memoryLocation == 1) return 0;

            var ringAndGridsize = GetRingNumber(memoryLocation);
            var ring = ringAndGridsize.Item1;
            var gridSize = ringAndGridsize.Item2;

            var minNumInRing = (gridSize * gridSize) - ((gridSize - 1) * (gridSize - 1)) + 1;
            var maxNumInRing = (gridSize * gridSize);

            var stepsToMiddleOfLine = GetStepsToMiddle(memoryLocation, gridSize, minNumInRing, maxNumInRing);

            return stepsToMiddleOfLine + ring;
        }

        private Tuple<int, int> GetRingNumber(int memoryLocation) {
            var ring = 0;
            var allocatedMemory = 1;
            var gridSize = 1;

            // Can probably be calculated without a loop?
            while (allocatedMemory < memoryLocation) 
            {
                ring++;
                gridSize += 2;
                allocatedMemory = gridSize * gridSize;
            }

            return new Tuple<int, int>(ring, gridSize);
        }

        private int GetStepsToMiddle(int memoryLocation, int gridSize, int minNumInRing, int maxNumInRing)
        {
            var sideMinNum = minNumInRing;
            var sideMaxNum = minNumInRing + (gridSize - 1);

            while (memoryLocation > sideMaxNum) 
            {
                sideMinNum += gridSize - 1;
                sideMaxNum += gridSize - 1;
            }

            return Math.Abs(sideMinNum + ((gridSize - 2) / 2) - memoryLocation);
        }
    }
}
