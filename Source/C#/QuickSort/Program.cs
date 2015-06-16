using System;
using System.Collections.Generic;

namespace QuickSort
{
    static class Program
    {
        static void Main()
        {
            var random = new Random();
            var values = new List<int>();

            for (var i = 0; i < 100000; i++)
            {
                values.Add(random.Next(100000));
            }

            SortAlgorithms.QuicksortParallel(values, 0, values.Count - 1);

            values.ForEach(Console.WriteLine);
        }
    }
}
