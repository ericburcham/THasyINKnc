using System;
using System.Threading;

namespace ThreadPool1
{
    internal static class Program
    {
        private static void Main()
        {
            var waitCallback = new WaitCallback(WriteMessage);
            ThreadPool.QueueUserWorkItem(waitCallback, "Hello, world!");

            lock (_lock)
            {
                Console.WriteLine("Waiting for ThreadPool thread.");
                Monitor.Wait(_lock);
            }

            Console.WriteLine("Finished.");
        }

        private static void WriteMessage(object message)
        {
            lock (_lock)
            {
                Thread.Sleep(2000);
                Console.WriteLine(message);
                Monitor.Pulse(_lock);
            }
        }

        private static readonly object _lock = new object();
    }
}