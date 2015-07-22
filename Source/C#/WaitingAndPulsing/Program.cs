using System;
using System.Threading;

namespace WaitingAndPulsing
{
    internal static class Program
    {
        private static void Main()
        {
            _sharedResource = new ProducerConsumer<int>();

            var producerThread = new Thread(ProducerJob);
            var consumerThread = new Thread(ConsumerJob);

            consumerThread.Start();
            producerThread.Start();

            producerThread.Join();
            consumerThread.Join();
        }

        private static void ProducerJob()
        {
            var random = new Random(0);
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("Producing {0}", i);
                _sharedResource.Produce(i);
                Thread.Sleep(random.Next(250));
            }
        }

        private static void ConsumerJob()
        {
            Thread.Sleep(1000);
            var random = new Random(1);
            for (var i = 0; i < 10; i++)
            {
                var number = _sharedResource.Consume();
                Console.WriteLine("\t\t\t\tConsuming {0}", number);
                Thread.Sleep(random.Next(500));
            }
        }

        private static ProducerConsumer<int> _sharedResource;
    }
}