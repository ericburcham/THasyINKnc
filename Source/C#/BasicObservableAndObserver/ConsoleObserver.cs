using System;

namespace BasicObservableAndObserver
{
    public class ConsoleObserver<T> : IObserver<T>
    {
        private readonly string _name;

        public ConsoleObserver(string name)
        {
            _name = name;
        }

        public void OnNext(T value)
        {
            Console.WriteLine("{0} - Received value: {1}", _name, value);
        }

        public void OnError(Exception ex)
        {
            Console.WriteLine("{0} - Received exception: {1}", _name, ex.Message);
        }

        public void OnCompleted()
        {
            Console.WriteLine("{0} - Sequence terminated.", _name);
        }
    }
}