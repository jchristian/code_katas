using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Machine.Specifications;
using NUnit.Framework;
using developwithpassion.specifications.moq;

namespace console.tests
{
    [TestFixture]
    public class BinarySearchPerformanceTests
    {
        [Test]
        public void test_on_randomly_selected_lists()
        {
            var searcher = new RecursiveSearcher();

            TestPerformance(searcher, size =>
            {
                var random = new Random();
                return Enumerable.Range(0, size).Select(x => random.Next(-10000000, 10000000)).OrderBy(x => x).ToArray();
            });
        }

        [Test]
        public void test_on_lists_with_all_the_same_value()
        {
            var searcher = new RecursiveSearcher();

            TestPerformance(searcher, size => Enumerable.Range(0, size).Select(x => 0).OrderBy(x => x).ToArray());
        }

        void TestPerformance(RecursiveSearcher searcher, Func<int, IEnumerable<int>> generate_list)
        {
            var sizes = new[] { 25000, 50000, 100000, 200000, 400000, 800000 };
            foreach (var size in sizes)
            {
                var number_of_iterations = 20;
                long total_time_elapsed = 0;
                for (int i = 0; i < number_of_iterations; i++)
                {
                    var ordered_list = generate_list(size);
                    var key = ordered_list.ElementAt(new Random().Next(0, size - 1));
                    var stopwatch = new Stopwatch();

                    stopwatch.Start();
                    searcher.Find(ordered_list, key);
                    stopwatch.Stop();
                    total_time_elapsed += stopwatch.ElapsedMilliseconds;
                }
                var average_time = total_time_elapsed / number_of_iterations;
                Console.WriteLine("It took an average of {0}ms to search {1} items", average_time, size);
            }
        }
    }
}
