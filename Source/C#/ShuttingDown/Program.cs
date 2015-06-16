using System;
using System.Threading;

namespace ShuttingDown
{
    static class Program
    {
        static void Main()
        {
            var writer = new DirectoryWriter(@"C:\Users\Eric\OneDrive");
            var threadStart = new ThreadStart(writer.Write);
            var worker = new Thread(threadStart);
            worker.Start();
            Thread.Sleep(50);
            Console.WriteLine(writer.Stopped);
            writer.Stop();
            worker.Join();
            Console.WriteLine(writer.Stopped);
        }
    }
}
