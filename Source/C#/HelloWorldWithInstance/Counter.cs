using System;
using System.Threading;

namespace HelloWorldWithInstance
{
    public class Counter
    {
        public void Count()
        {
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("Worker thread: {0}", i);
                Thread.Sleep(50);
            }
        }
    }
}