namespace BasicObservableAndObserver
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new SequenceOfIntegers(10);

            numbers.Subscribe(new ConsoleObserver<int>("Observer 1"));
            numbers.Subscribe(new ConsoleObserver<int>("Observer 2"));
            numbers.Subscribe(new ConsoleObserver<int>("Observer 3"));

            numbers.Iterate();
        }
    }
}
