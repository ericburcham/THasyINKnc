using System;
using System.Threading;

namespace IncrementingCounts1
{
    internal static class Program
    {
        private static void Main()
        {
            var threadStart = new ThreadStart(CountingThread);
            var worker = new Thread(threadStart);
            worker.Start();

            for (var i = 0; i < 5; i++)
            {
                _count++;
            }

            worker.Join();
            Console.WriteLine("Count is: {0}", _count);
        }

        private static void CountingThread()
        {
            for (var i = 0; i < 5; i++)
            {
                _count++;
            }
        }

        private static int _count;
    }
}