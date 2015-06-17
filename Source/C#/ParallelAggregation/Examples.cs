using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace ParallelAggregation
{
    public class Examples
    {
        public void ParallelForSum()
        {
            var lockObject = new object();
            var sumOfSquares = 0;

            Parallel.For(
                0,
                10,
                () => 0,
                (i, loopState, partialResult) => Square(i) + partialResult,
                localPartialSum =>
                    {
                        lock (lockObject)
                        {
                            sumOfSquares += localPartialSum;
                        }
                    });
            Console.WriteLine(sumOfSquares);
        }

        public void ParallelForeachSum()
        {
            var numbers = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var lockObject = new object();
            var sumOfSquares = 0;

            Parallel.ForEach(
                // The enumerable to operate on
                numbers,

                // The initial partial results
                () => 0,

                // The iteration definition
                (i, loopState, partialResult) => Square(i) + partialResult,

                // The final step of each local context
                localPartialSum =>
                    {
                        // Enforce serial access to single, shared result
                        lock (lockObject)
                        {
                            sumOfSquares += localPartialSum;
                        }
                    });

            Console.WriteLine(sumOfSquares);
        }

        public void PlinqSum()
        {
            var numbers = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var sumOfSquares = numbers.AsParallel().Select(i => Square(i)).Sum();
            Console.WriteLine(sumOfSquares);
        }

        public void ParallelCustomAggregator()
        {
            var numbers = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var sumOfSquares = numbers.AsParallel().Select(i => Square(i)).Aggregate(0, (sum, value) => sum + value);
            Console.WriteLine(sumOfSquares);
        }

        private int Square(int i)
        {
            return i * i;
        }
    }
}
