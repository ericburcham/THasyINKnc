using System;

namespace BasicObservableAndObserver
{
    public class ConsoleObserver<T> : IObserver<T>
    {
        public void OnNext(T value)
        {
            Console.WriteLine("Received value: {0}", value);
        }

        public void OnError(Exception ex)
        {
            Console.WriteLine("Received exception: {0}", ex.Message);
        }

        public void OnCompleted()
        {
            Console.WriteLine("Sequence terminated.");
        }
    }
}