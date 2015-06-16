using System;
using System.Threading;

namespace WaitingAndPulsing
{
    internal static class Program
    {
        private static void Main()
        {
            _queue = new ProducerConsumer();

            var producerThread = new Thread(ProducerJob);
            var consumerThread = new Thread(ConsumerJob);

            producerThread.Start();
            consumerThread.Start();

            producerThread.Join();
            consumerThread.Join();
        }

        private static void ProducerJob()
        {
            var random = new Random(0);
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("Producing {0}", i);
                _queue.Produce(i);
                Thread.Sleep(random.Next(500));
            }
        }

        private static void ConsumerJob()
        {
            var random = new Random(1);
            for (var i = 0; i < 10; i++)
            {
                var number = _queue.Consume();
                Console.WriteLine("\t\t\t\tConsuming {0}", number);
                Thread.Sleep(random.Next(500));
            }
        }

        private static ProducerConsumer _queue;
    }
}