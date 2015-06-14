using System.Threading;

namespace ViaAnonymousMethod
{
    internal static class Program
    {
        private static void Main()
        {
            // Using a constructor
            var googleUrl = "http://www.google.com";
            var urlFetcher = new UrlFetcher(googleUrl);
            var thread1 = new Thread(urlFetcher.Fetch);
            thread1.Start();

            // Using an anonymous method
            var threadStart = new ThreadStart(delegate { urlFetcher.FetchUrl(googleUrl); });
            var thread2 = new Thread(threadStart);
            thread2.Start();

            // Using parameterized thread start
            var parameterizedThreadStart = new ParameterizedThreadStart(urlFetcher.FetchUrl);
            var thread3 = new Thread(parameterizedThreadStart);
            thread3.Start(googleUrl);

            // Join threads
            thread1.Join();
            thread2.Join();
            thread3.Join();
        }
    }
}