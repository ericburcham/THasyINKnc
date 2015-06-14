using System.Threading;

namespace ViaAnInstance
{
    internal static class Program
    {
        private static void Main()
        {
            var urlFetcher = new UrlFetcher("http://www.google.com");
            var thread = new Thread(urlFetcher.Fetch);

            thread.Start();
            thread.Join();
        }
    }
}
