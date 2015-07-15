namespace Futures
{
    class Program
    {
        static void Main()
        {
            var examples = new Examples();
            examples.Sequential();
            examples.WithFutures();
            examples.WithContinuation();
        }
    }
}
