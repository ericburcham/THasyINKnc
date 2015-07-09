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

            var random = new Random(DateTime.Now.Millisecond);
            var chars = "HELLO".ToCharArray();
            foreach (var c in chars)
            {
                Console.Write(c);
                Thread.Sleep(random.Next(100, 500));
            }

            workerThread.Join();
            Console.ReadKey();
        }

        private static void ThreadJob()
        {
            var chars = "world".ToCharArray();
            var random = new Random(DateTime.Now.Millisecond);
            foreach (var c in chars)
            {
                Console.Write(c);
                Thread.Sleep(random.Next(100, 500));
            }
        }
    }
}
