using System;

namespace ParallelTasks
{
    static class Program
    {
        static void Main()
        {
            var examples = new Examples();

            Console.WriteLine("Start tasks with Parallel.Invoke");
            examples.ParallelInvoke();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Start tasks with Task.Factory.StartNew");
            examples.TaskFactoryStartNew();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Do image processing steps in parallel");
            examples.ParallelInvokeImageProcessing();
        }
    }
}
