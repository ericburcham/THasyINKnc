using System.Threading;

namespace ViaAnonymousMethod
{
    internal static class Program
    {
        private static void Main()
        {
            var thread1 = new Thread(new ThreadStart(delegate { FetchUrlInNewThread("http://www.yahoo.com"); }));
            var thread2 = new Thread(new ThreadStart(delegate { FetchUrlInNewThread("http://www.stackoverflow.com"); }));
            var thread3 = new Thread(new ThreadStart(delegate { FetchUrlInNewThread("http://www.google.com"); }));

            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
        }

        private static void FetchUrlInNewThread(string url)
        {
            var urlFetcher = new UrlFetcher();
            urlFetcher.FetchUrl(url);
        }
    }
}
