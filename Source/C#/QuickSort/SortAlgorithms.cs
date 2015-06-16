using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickSort
{
    internal static class SortAlgorithms
    {
        private static void Swap<T>(IList<T> array, int i, int j)
        {
            var tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }

        private static int Partition<T>(IList<T> array, int low, int high) where T : IComparable<T>
        {
            var pivotPosition = (high + low) / 2;
            var pivot = array[pivotPosition];

            Swap(array, low, pivotPosition);

            var left = low;
            for (var i = low + 1; i <= high; i++)
            {
                if (array[i].CompareTo(pivot) >= 0)
                {
                    continue;
                }

                left++;
                Swap(array, i, left);
            }

            Swap(array, low, left);

            return left;
        }

        public static void Quicksort<T>(IList<T> array, int left, int right) where T : IComparable<T>
        {
            if (right <= left)
            {
                return;
            }

            var pivot = Partition(array, left, right);

            Quicksort(array, left, pivot - 1);
            Quicksort(array, pivot + 1, right);
        }

        public static void QuicksortParallel<T>(IList<T> array, int left, int right) where T : IComparable<T>
        {
            const int ParallelThreshold = 512;

            if (right <= left)
            {
                return;
            }

            if (right - left < ParallelThreshold)
            {
                Quicksort(array, left, right);
            }

            else
            {
                var pivot = Partition(array, left, right);

                Parallel.Invoke(
                    () => QuicksortParallel(array, left, pivot - 1),
                    () => QuicksortParallel(array, pivot + 1, right));
            }
        }
    }
}
