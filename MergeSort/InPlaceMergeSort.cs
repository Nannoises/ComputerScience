using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MergeSort
{
    public static class InPlaceMergeSort
    {
        public static void Sort(int[] toSort)
        {
            if (toSort == null || toSort.Length <= 0)
                return;
            
            var scratch = new int[toSort.Length];
            Worker(toSort, scratch, 0, toSort.Length - 1);
        }

        private static void Worker(int[] toSort, int[] scratch, int begin, int end)
        {
            if (end == begin)            
                return;

            var middle = (end + begin)/2;
            Worker(toSort, scratch, begin, middle);
            Worker(toSort, scratch, middle + 1, end);

            Merge(toSort, scratch, begin, middle, end);
            CopyArray(scratch, toSort, begin, end);
        }

        private static void Merge(int[] a, int[] b, int begin, int middle, int end)
        {
            var ai = begin;
            var bi = middle + 1;

            var total = end - begin + 1;

            for (var i = begin; i < begin + total; i++)
            {
                if (ai > middle)
                    b[i] = a[bi++];
                else if (bi > end)
                    b[i] = a[ai++];
                else
                    b[i] = a[ai] < a[bi] ? a[ai++] : a[bi++];
            }            
        }

        private static void CopyArray(int[] source, int[] destination, int start, int end)
        {
            for (var i = start; i < end + 1; i++)
                destination[i] = source[i];
        }
    }
}
