using System.Threading;

namespace ViaParameterizedThreadStart
{
    internal static class Program
    {
        private static void Main()
        {
            var urlFetcher = new UrlFetcher();
            var parameterizedThreadStart = new ParameterizedThreadStart(urlFetcher.FetchUrl);
            var thread = new Thread(parameterizedThreadStart);

            thread.Start("http://www.google.com");
            thread.Join();
        }
    }
}