using System.Threading;

namespace ViaParameterizedThreadStart
{
    internal static class Program
    {
        private static void Main()
        {
            var urlFetcher = new UrlFetcher();
            var parameterizedThreadStart = new ParameterizedThreadStart(urlFetcher.FetchUrl);
            var thread1 = new Thread(parameterizedThreadStart);
            var thread2 = new Thread(parameterizedThreadStart);
            var thread3 = new Thread(parameterizedThreadStart);

            thread1.Start("http://www.stackoverflow.com");
            thread2.Start("http://www.msdn.microsoft.com");
            thread3.Start("http://www.google.com");

            thread1.Join();
            thread2.Join();
            thread3.Join();
        }
    }
}