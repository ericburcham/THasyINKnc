using System.Threading;

namespace ViaAnInstance
{
    internal static class Program
    {
        private static void Main()
        {
            var googleUrl = "http://www.google.com";
            var urlFetcher = new UrlFetcher(googleUrl);
            var thread1 = new Thread(urlFetcher.Fetch);
            thread1.Start();
            thread1.Join();
        }
    }
}
