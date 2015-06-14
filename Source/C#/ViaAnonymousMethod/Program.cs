using System.Threading;

namespace ViaAnonymousMethod
{
    internal static class Program
    {
        private static void Main()
        {
            var urlFetcher = new UrlFetcher();
            var threadStart = new ThreadStart(delegate { urlFetcher.FetchUrl("http://www.google.com"); });
            var thread = new Thread(threadStart);

            thread.Start();
            thread.Join();
        }
    }
}
