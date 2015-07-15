using System;
using System.Threading;

namespace ResetEvents
{
    internal static class Program
    {
        private static void Main()
        {
            WaitHandle[] events = new WaitHandle[10];
            for (var i = 0; i < events.Length; i++)
            {
                events[i] = new ManualResetEvent(false);
                var runner = new Runner((ManualResetEvent)events[i], i);
                var worker = new Thread(runner.Run);
                worker.Start();
            }

            var index = WaitHandle.WaitAny(events);

            Console.WriteLine("***** The winner is {0} *****", index);

            WaitHandle.WaitAll(events);
            Console.WriteLine("All finished!");
        }
    }

    internal class Runner
    {
        internal Runner(ManualResetEvent resetEvent, int id)
        {
            _resetEvent = resetEvent;
            _id = id;
        }

        internal void Run()
        {
            for (var i = 0; i < 10; i++)
            {
                int sleepTime;

                // Don't know, don't care.
                lock (_lock)
                {
                    sleepTime = _random.Next(2000);
                }
                Thread.Sleep(sleepTime);
                Console.WriteLine("Runner {0} at stage {1}", _id, i);
            }

            _resetEvent.Set();
        }

        private static readonly object _lock = new object();

        private static readonly Random _random = new Random();

        private readonly ManualResetEvent _resetEvent;

        private readonly int _id;
    }
}