using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace QuickSort
{
    internal static class Program
    {
        private static void Main()
        {
            var arraySize = 4000000;
            var maxValue = 10000;
            var random = new Random(DateTime.Now.Millisecond);
            var values = new List<int>();

            for (var i = 0; i < arraySize; i++)
            {
                values.Add(random.Next(maxValue));
            }

            var leftIndex = 0;
            var rightIndex = values.Count - 1;

            var sw = new Stopwatch();
            sw.Start();

            // Change with of the following 2 lines is commented to see how slow the non-parallel version is.  Watch the CPU meter, too.
            // SortAlgorithms.Quicksort(values, leftIndex, rightIndex);
            SortAlgorithms.QuicksortParallel(values, leftIndex, rightIndex);

            sw.Stop();
            foreach (var value in values.Where(value => value % 1000 == 0))
            {
                Console.WriteLine(value);
            }
            Console.WriteLine("Sort {0:N} items in {1}", arraySize, sw.Elapsed.ToString("g"));
        }
    }
}