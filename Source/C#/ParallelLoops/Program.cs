using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLoops
{
    static class Program
    {
        static void Main()
        {
            var psuedo = new PsuedoCode();

            //Console.WriteLine("Sequantial for loop");
            //psuedo.SequentialFor();

            //Console.WriteLine("----------------------------");
            //Console.WriteLine("Parallel for loop.  Note that order is not quaranteed.");
            //psuedo.ParallelFor();

            //Console.WriteLine("Press any key to continue.");
            //Console.ReadKey();
            //Console.Clear();
            //Console.WriteLine("----------------------------");
            //Console.WriteLine("Sequantial foreach");
            //psuedo.SequentialForeach();

            //Console.WriteLine("----------------------------");
            //Console.WriteLine("Parallel foreach.  Note that order is not quaranteed.");
            //psuedo.ParallelForeach();

            //Console.WriteLine("Press any key to continue.");
            //Console.ReadKey();
            //Console.Clear();
            //Console.WriteLine("----------------------------");
            //Console.WriteLine("LINQ select and iterate.");
            //psuedo.LinqSelectAndIterate();

            //Console.WriteLine("----------------------------");
            //Console.WriteLine("PLINQ select and iterate.  Note that order is not quaranteed.");
            //psuedo.PlinqSelectAndIterate();

            //Console.WriteLine("Press any key to continue.");
            //Console.ReadKey();
            //Console.Clear();
            //Console.WriteLine("----------------------------");
            //Console.WriteLine("Internally breaking a loop.");
            //psuedo.InternalBreak();

            //Console.WriteLine("Press any key to continue.");
            //Console.ReadKey();
            //Console.Clear();
            //Console.WriteLine("----------------------------");
            //Console.WriteLine("Externally breaking a loop.");
            //var cancellationTokenSource = new CancellationTokenSource();
            //var worker = new Thread(() => psuedo.ExternalBreak(cancellationTokenSource));

            //worker.Start();
            //Thread.Sleep(1000);
            //cancellationTokenSource.Cancel();

            //worker.Join();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Handling aggregate exceptions.");
            psuedo.ExceptionHandling();

            //Console.WriteLine("Press any key to continue.");
            //Console.ReadKey();
            //Console.Clear();
            //Console.WriteLine("----------------------------");
            //Console.WriteLine("Writing to a shared variable.");
            //psuedo.WritingToSharedVariables();

            //Console.WriteLine("Press any key to continue.");
            //Console.ReadKey();
            //Console.Clear();
            //Console.WriteLine("----------------------------");
            //Console.WriteLine("Missusing a property of an object model.");
            //psuedo.UsingPropertiesOfAnObjectModel();

            //Console.WriteLine("Press any key to continue.");
            //Console.ReadKey();
            //Console.Clear();
            //Console.WriteLine("----------------------------");
            //Console.WriteLine("Loop-carried dependence.");
            //psuedo.LoopCarriedDependence();
        }
    }
}
