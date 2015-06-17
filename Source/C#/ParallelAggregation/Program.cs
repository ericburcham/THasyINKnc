using System;

namespace ParallelAggregation
{
    static class Program
    {
        static void Main()
        {
            var examples = new Examples();

            Console.WriteLine("Parallel sum with custom aggregator");
            examples.ParallelCustomAggregator();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Parallel sum of for loop");
            examples.ParallelForSum();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Parallel sum of foreach loop");
            examples.ParallelForeachSum();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Plinq sum");
            examples.PlinqSum();
        }
    }
}
