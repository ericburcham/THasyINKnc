using System;

namespace Async2
{
    internal static class Program
    {
        private static void Main()
        {
            TestDelegate d = WriteMessage;

            var asyncResult = d.BeginInvoke("Hello, world!", null, null);

            Console.WriteLine("Main thread continuing to execute...");

            // This will block until the asyncResult 
            var result = d.EndInvoke(asyncResult);

            Console.WriteLine("Delegate returned {0}", result);
        }

        private static int WriteMessage(string message)
        {
            Console.WriteLine(message);
            return 5;
        }

        private delegate int TestDelegate(string s);
    }
}