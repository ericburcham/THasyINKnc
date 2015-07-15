using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ParallelLoops.Models;

using Resources;

namespace ParallelLoops
{
    internal class Examples
    {
        // Here we have the basic syntax for getting stuff done...
        public void SequentialFor()
        {
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
        }

        public void ParallelFor()
        {
            Parallel.For(0, 10, Console.WriteLine);
        }

        public void SequentialForeach()
        {
            var numbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            foreach (var i in numbers)
            {
                Console.WriteLine(i);
            }
        }

        public void ParallelForeach()
        {
            var numbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Parallel.ForEach(numbers, Console.WriteLine);
        }

        public void LinqSelectAndIterate()
        {
            var numbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var squares = numbers.Select(i => Functions.Square(i)).ToList();
            squares.ForEach(Console.WriteLine);
        }

        public void PlinqSelectAndIterate()
        {
            var numbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var squares = numbers.AsParallel().Select(i => Functions.Square(i));
            Parallel.ForEach(squares, Console.WriteLine);
        }

        public void InternalBreak()
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = Parallel.For(
                0,
                100,
                (i, loopState) =>
                    {
                        if (sw.ElapsedMilliseconds >= 50)
                        {
                            loopState.Break();
                            return;
                        }

                        Console.WriteLine(i);
                        Thread.Sleep(20);
                    });

            if (!result.IsCompleted && result.LowestBreakIteration.HasValue)
            {
                Console.WriteLine("Loop broke at iteration: {0}", result.LowestBreakIteration.Value);
            }
        }

        public void ExternalBreak(CancellationTokenSource cancellationTokenSource)
        {
            var cancellationToken = cancellationTokenSource.Token;
            var options = new ParallelOptions { CancellationToken = cancellationToken };

            try
            {
                Parallel.For(
                    0,
                    100,
                    options,
                    i =>
                        {
                            if (cancellationToken.IsCancellationRequested)
                            {
                                return;
                            }
                            Console.WriteLine(i);
                            Thread.Sleep(500);
                        });
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ExceptionHandling()
        {

            try
            {
                Parallel.For(
                    0,
                    10,
                    i =>
                        {
                            Thread.Sleep(10);
                            if (i % 2 == 0)
                            {
                                Console.WriteLine(i);
                            }
                            else
                            {
                                throw new InvalidOperationException(string.Format("Step {0} failed.", i));
                            }
                        });

            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.Message);
                foreach (var innerException in ex.InnerExceptions)
                {
                    Console.WriteLine(innerException.Message);
                }
            }
        }

        // Some common things to look for if your parallel loop has unexpected behavior:
        public void WritingToSharedVariables()
        {
            var count = 0;

            // Count is shared between threads.  It is possible for threads to
            // simultaneously try to modify its value, in which the lagging thread
            // will overwrite the progress of the leading thread.
            Parallel.For(
                0,
                10,
                i =>
                    {
                        // count += i;
                        // This is what is really happening with the += operator.
                        var temp = count;
                        Thread.Sleep(10);
                        temp += i;
                        Thread.Sleep(10);
                        count = temp;
                    });

            // Here we'd expect 45.
            Console.WriteLine(count);
        }

        public void LoopCarriedDependence()
        {
            var data = new[] { 3, 7, 3, 1 };

            for (var i = 0; i < data.Length; i++)
            {
                Console.WriteLine("Position: {0}, Value: {1}", i, data[i]);
            }

            Console.WriteLine("Last position should be: {0}", data.Sum());
            Console.WriteLine();

            Parallel.For(
                1,
                data.Length,
                i =>
                    {
                        var newValue = data[i] + data[i - 1];
                        Thread.Sleep(10);
                        data[i] = newValue;
                        Thread.Sleep(10);
                        Console.WriteLine("Position: {0}, Value: {1}", i, data[i]);
                    });
            Console.WriteLine("Last position is: {0}", data[data.Length - 1]);
        }

        public void UsingPropertiesOfAnObjectModel()
        {
            var parent = new Parent { Id = 1, Name = "Parent 1" };

            parent.Children.AddRange(
                new List<Child>
                    {
                        new Child { Id = 1, Name = "Child 1", Parent = parent },
                        new Child { Id = 2, Name = "Child 2", Parent = parent },
                        new Child { Id = 3, Name = "Child 3", Parent = parent }
                    });

            // If an object being processed by a loop body exposes properties,
            // we need to know if those properties refer to shared state or
            // state that is local to the object itself.  A property name "parent"
            // is likey to refer to global state.
            Parallel.ForEach(parent.Children, child => child.Parent.AddChildId(child.Id));

            Console.WriteLine(parent.SumOfChildIds);
        }
    }
}