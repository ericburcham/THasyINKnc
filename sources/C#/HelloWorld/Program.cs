using System;
using System.Threading;

namespace HelloWorld
{
    internal class Program
    {
        private static void Main()
        {
            var job = new ThreadStart(ThreadJob);
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

        private static void ThreadJob()
        {
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("Worker thread: {0}", i);
                Thread.Sleep(50);
            }
        }
    }
}