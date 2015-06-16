using System.Threading;

namespace ViaAnonymousMethod
{
    internal static class Program
    {
        private static void Main()
        {
            var thread1 = new Thread(new ThreadStart(delegate { FetchUrlInNewThread("http://www.yahoo.com", 5); }));
            var thread2 = new Thread(new ThreadStart(delegate { FetchUrlInNewThread("http://www.stackoverflow.com", 10); }));
            var thread3 = new Thread(() => FetchUrlInNewThread("http://www.google.com", 15));

            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
        }

        private static void FetchUrlInNewThread(string url, int count)
        {
            var urlFetcher = new UrlFetcher();
            urlFetcher.FetchUrl(url, count);
        }
    }
}
