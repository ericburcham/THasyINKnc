using System;
using System.Threading;

namespace IncrementingCounts4
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
                lock (_countLock)
                {
                    var temp = _count;
                    Thread.Sleep(10);
                    Console.WriteLine("Read count: {0}", temp);

                    temp++;
                    Thread.Sleep(10);
                    Console.WriteLine("Update temp: {0}", temp);

                    _count = temp;
                    Thread.Sleep(10);
                    Console.WriteLine("Updated count: {0}", _count);
                }
            }

            worker.Join();
            Console.WriteLine("Count is: {0}", _count);
        }

        private static void CountingThread()
        {
            for (var i = 0; i < 5; i++)
            {
                lock (_countLock)
                {
                    var temp = _count;
                    Thread.Sleep(10);
                    Console.WriteLine("\t\t\tRead count: {0}", temp);

                    temp++;
                    Thread.Sleep(10);
                    Console.WriteLine("\t\t\tUpdate temp: {0}", temp);

                    _count = temp;
                    Thread.Sleep(10);
                    Console.WriteLine("\t\t\tUpdated count: {0}", _count);
                }
            }
        }

        private static int _count;

        private static readonly object _countLock = new object();
    }
}
