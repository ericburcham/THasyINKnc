using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickSort
{
    internal static class SortAlgorithms
    {
        public static void Quicksort<T>(IList<T> array, int leftIndex, int rightIndex) where T : IComparable<T>
        {
            if (rightIndex <= leftIndex)
            {
                return;
            }

            var pivot = Partition(array, leftIndex, rightIndex);

            Quicksort(array, leftIndex, pivot - 1);
            Quicksort(array, pivot + 1, rightIndex);
        }

        public static void QuicksortParallel<T>(IList<T> array, int leftIndex, int rightIndex) where T : IComparable<T>
        {
            const int ParallelThreshold = 1024;

            if (rightIndex <= leftIndex)
            {
                return;
            }

            if (rightIndex - leftIndex < ParallelThreshold)
            {
                Quicksort(array, leftIndex, rightIndex);
            }

            else
            {
                var pivot = Partition(array, leftIndex, rightIndex);

                Parallel.Invoke(
                    () => QuicksortParallel(array, leftIndex, pivot - 1),
                    () => QuicksortParallel(array, pivot + 1, rightIndex));
            }
        }

        private static int Partition<T>(IList<T> array, int lowIndex, int highIndex) where T : IComparable<T>
        {
            var pivotPosition = (highIndex + lowIndex) / 2;
            var pivot = array[pivotPosition];

            array.Swap(lowIndex, pivotPosition);

            var left = lowIndex;
            for (var i = lowIndex + 1; i <= highIndex; i++)
            {
                if (array[i].CompareTo(pivot) >= 0)
                {
                    continue;
                }

                left++;
                array.Swap(i, left);
            }

            array.Swap(lowIndex, left);

            return left;
        }

        private static void Swap<T>(this IList<T> array, int firstIndex, int secondIndex)
        {
            var tmp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = tmp;
        }
    }
}
