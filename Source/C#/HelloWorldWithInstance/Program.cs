using System;
using System.Threading;

namespace HelloWorldWithInstance
{
    internal class Program
    {
        private static void Main()
        {
            var counter = new Counter();
            var job = new ThreadStart(counter.Count);
            var workerThread = new Thread(job);
            workerThread.Start();

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("Main thread: {0}", i);
                Thread.Sleep(200);
            }

            workerThread.Join();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}