using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var toSort = new[] {5, 6, 2, 3, 1, 8, 9, 10, 4, 7};
            Console.Write("Before sort: ");
            PrintArray(toSort);
            Console.WriteLine();
            var sorted = MergeSort.Sort(toSort);
            Console.Write("After sort: ");
            PrintArray(sorted);
            Console.ReadLine();
        }

        private static void PrintArray(int[] toPrint)
        {
            foreach (var i in toPrint)
            {
                Console.Write(i + " ");
            }
        }
    }
}
