using System;
using System.Threading;

namespace Mutex2
{
    internal static class Program
    {
        private static void Main()
        {
            bool createdNew;

            using (new Mutex(true, @"Local\Mutex", out createdNew))
            {
                if (!createdNew)
                {
                    Console.WriteLine("Other instance detected.");
                    Thread.Sleep(5000);
                    return;
                }

                Console.WriteLine("We're the only instance running.");
                for (var i = 0; i < 10; i++)
                {
                    Console.WriteLine(i);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}