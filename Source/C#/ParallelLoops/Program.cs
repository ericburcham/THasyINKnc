using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLoops
{
    static class Program
    {
        static void Main()
        {
            var examples = new Examples();

            Console.WriteLine("Sequantial for loop");
            examples.SequentialFor();

            Console.WriteLine("----------------------------");
            Console.WriteLine("Parallel for loop.  Note that order is not guaranteed.");
            examples.ParallelFor();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Sequantial foreach");
            examples.SequentialForeach();

            Console.WriteLine("----------------------------");
            Console.WriteLine("Parallel foreach.  Note that order is not guaranteed.");
            examples.ParallelForeach();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("LINQ select and iterate.");
            examples.LinqSelectAndIterate();

            Console.WriteLine("----------------------------");
            Console.WriteLine("PLINQ select and iterate.  Note that order is not guaranteed.");
            examples.PlinqSelectAndIterate();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Internally breaking a loop.");
            examples.InternalBreak();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Externally breaking a loop.");
            var cancellationTokenSource = new CancellationTokenSource();
            var worker = new Thread(() => examples.ExternalBreak(cancellationTokenSource));

            worker.Start();
            Thread.Sleep(1000);
            cancellationTokenSource.Cancel();
            worker.Join();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Handling aggregate exceptions.");
            examples.ExceptionHandling();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Writing to a shared variable.");
            examples.WritingToSharedVariables();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Missusing a property of an object model.");
            examples.UsingPropertiesOfAnObjectModel();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Loop-carried dependence.");
            examples.LoopCarriedDependence();
        }
    }
}
