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

            Console.WriteLine("Locking second");
            lock (_second)
            {
                Console.WriteLine("Locked second");
                Console.WriteLine("Locking first");
                lock (_first)
                {
                    Console.WriteLine("Locked first");
                }
                Console.WriteLine("Released first");
            }
            Console.WriteLine("Released second");

            worker.Join();
        }

        private static void ThreadJob()
        {
            Console.WriteLine("\t\t\t\tLocking first");
            lock (_first)
            {
                Console.WriteLine("\t\t\t\tLocked first");

                Thread.Sleep(1000);
                Console.WriteLine("\t\t\t\tLocking second");
                lock (_second)
                {
                    Console.WriteLine("\t\t\t\tLocked second");
                }
                Console.WriteLine("\t\t\t\tReleased second");
            }
            Console.WriteLine("\t\t\t\tReleased first");
        }

        private static readonly object _first = new object();

        private static readonly object _second = new object();
    }
}
