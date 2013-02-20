
using System;
using System.Collections.Generic;
using System.Linq;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = GetListFromUser();
            var key = GetKeyFromUser();
            ReportResult(key, new BinarySearcher().Find(array, key));
        }

        static void ReportResult(int key, int index)
        {
            Console.WriteLine("The first instance of {0} was found at position {1}", key, index);
        }

        static int GetKeyFromUser()
        {
            Console.WriteLine("Enter the key you wish to search for: ");
            return int.Parse(Console.ReadLine());
        }

        static IEnumerable<int> GetListFromUser()
        {
            Console.WriteLine("Enter an ordered list of integers separated by spaces: ");
            return Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        }
    }
}
