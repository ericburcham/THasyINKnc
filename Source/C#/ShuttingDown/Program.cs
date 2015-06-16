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

            Thread.Sleep(1000);
            writer.Stop();
            worker.Join();
        }
    }
}
