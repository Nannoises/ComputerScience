using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MergeSort
{
    public static class MergeSort
    {
        public static int[] Sort(int[] toSort)
        {
            return Worker(toSort, 0, toSort.Length - 1);
        }

        private static int[] Worker(int[] toSort, int begin, int end)
        {
            if (end == begin)
                return new[] {toSort[begin]};

            var middle = ((end - begin) / 2) + begin;            
            var left = Worker(toSort, begin, middle);
            var right = Worker(toSort, middle + 1, end);
            return Merge(left, right);
        }

        private static int[] Merge(int[] a, int[] b)
        {
            var ai = 0;
            var bi = 0;
            var toReturn = new int[a.Length + b.Length];

            for (var i = 0; i < toReturn.Length; i++)
            {
                if (ai == a.Length)
                    toReturn[i] = b[bi++];
                else if (bi == b.Length)
                    toReturn[i] = a[ai++];
                else
                    toReturn[i] = a[ai] < b[bi] ? a[ai++] : b[bi++];
            }

            return toReturn;
        }
    }
}
