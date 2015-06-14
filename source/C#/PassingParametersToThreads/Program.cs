using System.Threading;

namespace ViaAnInstance
{
    internal static class Program
    {
        private static void Main()
        {
            var thread1 = new Thread(new UrlFetcher("http://www.stackoverflow.com").Fetch);
            var thread2 = new Thread(new UrlFetcher("http://msdn.microsoft.com").Fetch);
            var thread3 = new Thread(new UrlFetcher("http://www.google.com").Fetch);

            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
        }
    }
}
