using System;
using System.Linq;
using System.Threading.Tasks;

using Resources;

namespace ParallelAggregation
{
    public class Examples
    {
        public void ParallelForSum()
        {
            var lockObject = new object();
            var sumOfSquares = 0;

            Parallel.For(
                // The inclusive min loop value
                0,

                // The exclusive man loop value
                10,

                // The initial partial results
                () => 0,

                // The iteration definition
                (i, loopState, partialResult) => i.Square() + partialResult,

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
                (i, loopState, partialResult) => i.Square() + partialResult,

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
            var sumOfSquares = numbers.AsParallel().Select(i => i.Square()).Sum();
            Console.WriteLine(sumOfSquares);
        }

        public void ParallelCustomAggregator()
        {
            var numbers = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var sumOfSquares = numbers.AsParallel().Select(i => i.Square()).Aggregate(0, (sum, value) => sum + value);
            Console.WriteLine(sumOfSquares);
        }
    }
}
