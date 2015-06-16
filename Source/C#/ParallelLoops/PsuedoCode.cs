using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelLoops
{
    internal class PsuedoCode
    {
        // Here we have the basic syntax for getting stuff done...
        public void SequentialFor()
        {
            var n = 10;
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
        }

        public void ParallelFor()
        {
            var n = 10;
            Parallel.For(0, n, i => Console.WriteLine(i));
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
            Parallel.ForEach(numbers, i => Console.WriteLine(i));
        }

        public void LinqSelectAndIterate()
        {
            var numbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var squares = numbers.Select(i => Square(i)).ToList();
            squares.ForEach(i => Console.WriteLine(i));
        }

        public void PlinqSelectAndIterate()
        {
            var numbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, };
            var squares = numbers.AsParallel().Select(i => Square(i));
            Parallel.ForEach(squares, result => Console.WriteLine(result));
        }

        // Some common things to look for if your parallel loop has unexpected behavior:
        public void LoopBodyDependency()
        {
            var count = 0;
            Parallel.For(0, 10, i => count += i);
            Console.WriteLine(count);
        }

        // Some internal methods
        private int Square(int i)
        {
            return i ^ 2;
        }
    }
}
