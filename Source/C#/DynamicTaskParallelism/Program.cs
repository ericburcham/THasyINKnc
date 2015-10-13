using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DynamicTaskParallelism
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Tree walking example:");
            Console.WriteLine();
            var one = new BinaryTree<int>(1);
            var two = new BinaryTree<int>(2);
            var three = new BinaryTree<int>(3);
            var four = new BinaryTree<int>(4);
            var five = new BinaryTree<int>(5);
            var six = new BinaryTree<int>(6);
            var seven = new BinaryTree<int>(7);
            var eight = new BinaryTree<int>(8);
            var nine = new BinaryTree<int>(9);

            one.Left = two;
            two.Left = three;
            two.Right = four;
            four.Left = five;
            four.Right = six;
            one.Right = seven;
            seven.Right = eight;
            eight.Right = nine;

            Action<int> action = i => Console.WriteLine("Walked: {0}", i);

            Console.WriteLine("Walking in serial:");
            one.Walk(action);

            Console.WriteLine();
            Console.WriteLine("Walking in parallel:");
            one.ParallelWalk(action);

            Console.WriteLine();
            Console.WriteLine("Walking while not empty:");
            one.ParallelWalkWhileNotEmpty(action);

            Console.WriteLine();
            Console.WriteLine("Walking with parent/child tasks:");
            one.ParallelWalkWithParentChildTasks(action);
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Array sorting example:");
            Console.WriteLine();
            var arraySize = 7500000;
            var maxValue = 1000000;
            var random = new Random(DateTime.Now.Millisecond);
            var sw = new Stopwatch();
            var values = new List<int>();
            for (var i = 0; i < arraySize; i++)
            {
                values.Add(random.Next(maxValue));
            }

            var valuesToSort = new int[arraySize];
            values.CopyTo(valuesToSort);

            Console.WriteLine("Sorting with Quicksort:");
            sw.Start();
            SortAlgorithms.Quicksort(valuesToSort);
            Console.WriteLine("Sort {0:N} items in {1}", arraySize, sw.Elapsed.ToString("g"));

            Console.WriteLine();
            Console.WriteLine("Sorting with Parallel Quicksort:");
            sw.Restart();
            SortAlgorithms.ParallelQuicksort(valuesToSort);
            Console.WriteLine("Sort {0:N} items in {1}", arraySize, sw.Elapsed.ToString("g"));
            sw.Stop();
        }
    }
}
