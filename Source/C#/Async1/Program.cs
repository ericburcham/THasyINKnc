using System;
using System.Threading;

namespace Async1
{
    internal static class Program
    {
        private static void Main()
        {
            TestDelegate testDelegate = WriteMessage;

            testDelegate.BeginInvoke("Hello, world!", Callback, testDelegate);

            // Make sure the callback has time to run.
            Thread.Sleep(1000);
        }

        private static int WriteMessage(string message)
        {
            Console.WriteLine(message);
            return 5;
        }

        private static void Callback(IAsyncResult asyncResult)
        {
            var testDelegate = (TestDelegate)asyncResult.AsyncState;
            Console.WriteLine("Delegate returned {0}", testDelegate.EndInvoke(asyncResult));
        }

        private delegate int TestDelegate(string s);
    }
}
