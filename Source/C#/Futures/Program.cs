namespace Futures
{
    class Program
    {
        static void Main(string[] args)
        {
            var examples = new Examples();
            examples.Sequantial();
            examples.WithFutures();
            examples.WithContinuation();
        }
    }
}
