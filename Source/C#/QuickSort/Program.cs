using System;
using System.Collections.Generic;

namespace QuickSort
{
    internal static class Program
    {
        private static void Main()
        {
            var random = new Random(DateTime.Now.Millisecond);
            var values = new List<int>();

            for (var i = 0; i < 1000; i++)
            {
                values.Add(random.Next(10000));
            }

            // Change with of the following 2 lines is commented to see how slow the non-parallel version is.  Watch the CPU meter, too.
            // SortAlgorithms.Quicksort(values, 0, values.Count - 1);
            SortAlgorithms.QuicksortParallel(values, 0, values.Count - 1);

            foreach (var i in values)
            {
                Console.WriteLine(i);
            }
        }
    }
}