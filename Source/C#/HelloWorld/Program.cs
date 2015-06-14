using System;
using System.Threading;

namespace HelloWorld
{
    internal static class Program
    {
        private static void Main()
        {
            var job = new ThreadStart(ThreadJob);
            var workerThread = new Thread(job);
            workerThread.Start();

            var random = new Random(100);
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("Hello main thread: {0}", i);
                Thread.Sleep(random.Next(10, 50));
            }

            workerThread.Join();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static void ThreadJob()
        {
            var random = new Random(200);
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("Hello worker thread: {0}", i);
                Thread.Sleep(random.Next(10, 50));
            }
        }
    }
}
