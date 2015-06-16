using System;
using System.Collections.Generic;

namespace QuickSort
{
    internal static class Program
    {
        private static void Main()
        {
            var random = new Random();
            var values = new List<int>();

            for (var i = 0; i < 10000000; i++)
            {
                values.Add(random.Next(100000));
            }

            // Change with of the following 2 lines is commented to see how slow the non-parallel version is.  Watch the CPU meter, too.
            // SortAlgorithms.Quicksort(values, 0, values.Count - 1);
            SortAlgorithms.QuicksortParallel(values, 0, values.Count - 1);

            foreach (var i in values)
            {
                // Use a reasonably large prime to limit the output.
                if (i % 7919 == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}