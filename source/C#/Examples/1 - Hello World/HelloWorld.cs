using System;
using System.Threading;

namespace Examples
{
    public class HelloWorld
    {
        public void Run()
        {
            var job = new ThreadStart(ThreadJob);
            var workerThread = new Thread(job);
            workerThread.Start();

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("Main thread: {0}", i);
                Thread.Sleep(200);
            }
        }

        private void ThreadJob()
        {
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("Worker thread: {0}", i);
                Thread.Sleep(100);
            }
        }
    }
}