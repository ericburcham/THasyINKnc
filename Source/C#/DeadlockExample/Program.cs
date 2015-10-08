using System;
using System.Threading;

namespace DeadlockExample
{
    internal static class Program
    {
        private static void Main()
        {
            var worker = new Thread(ThreadJob);
            worker.Start();

            Thread.Sleep(500);

            Console.WriteLine("Locking first");
            lock (_firstLock)
            {
                Console.WriteLine("Locked first");
                Console.WriteLine("Locking second");
                lock (_secondLock)
                {
                    Console.WriteLine("Locked second");
                }
                Console.WriteLine("Released second");
            }
            Console.WriteLine("Released first");

            worker.Join();
        }

        private static void ThreadJob()
        {
            Console.WriteLine("\t\t\t\tLocking second");
            lock (_secondLock)
            {
                Console.WriteLine("\t\t\t\tLocked second");

                Thread.Sleep(1000);
                Console.WriteLine("\t\t\t\tLocking first");
                lock (_firstLock)
                {
                    Console.WriteLine("\t\t\t\tLocked first");
                }
                Console.WriteLine("\t\t\t\tReleased first");
            }
            Console.WriteLine("\t\t\t\tReleased second");
        }

        private static readonly object _firstLock = new object();

        private static readonly object _secondLock = new object();
    }
}
