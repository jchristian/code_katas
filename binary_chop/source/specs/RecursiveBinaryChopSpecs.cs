using System;
using System.Collections.Generic;
using System.Linq;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.moq;
using Machine.Specifications;

namespace source.specs
{
    public class RecursiveBinaryChopSpecs
    {
        public abstract class concern : Observes<IFindAnItem,
                                            RecursiveBinaryChop> {}

        [Subject(typeof(RecursiveBinaryChop))]
        public class when_finding_the_item : concern
        {
            Establish c = () =>
            {
                test_cases = new List<Tuple<int, IList<int>>>();

                test_cases.Add(new Tuple<int, IList<int>>(-1, new List<int>()));
                test_cases.Add(new Tuple<int, IList<int>>(-1, new[] { 2 }));
                test_cases.Add(new Tuple<int, IList<int>>(0, new[] { item_to_find }));

                test_cases.Add(new Tuple<int, IList<int>>(0, new[] { item_to_find, 3, 5 }));
                test_cases.Add(new Tuple<int, IList<int>>(1, new[] { -1, item_to_find, 3 }));
                test_cases.Add(new Tuple<int, IList<int>>(2, new[] { -3, -1, item_to_find }));
                test_cases.Add(new Tuple<int, IList<int>>(-1, new[] { 2, 4, 6 }));
                test_cases.Add(new Tuple<int, IList<int>>(-1, new[] { 0, 2, 4 }));
                test_cases.Add(new Tuple<int, IList<int>>(-1, new[] { -2, 0, 2 }));
                test_cases.Add(new Tuple<int, IList<int>>(-1, new[] { -4, -2, 0 }));

                test_cases.Add(new Tuple<int, IList<int>>(0, new[] { item_to_find, 3, 5, 7 }));
                test_cases.Add(new Tuple<int, IList<int>>(1, new[] { -1, item_to_find, 3, 5 }));
                test_cases.Add(new Tuple<int, IList<int>>(2, new[] { -3, -1, item_to_find, 3 }));
                test_cases.Add(new Tuple<int, IList<int>>(3, new[] { -5, -3, -1, item_to_find }));
                test_cases.Add(new Tuple<int, IList<int>>(-1, new[] { 2, 4, 6, 8 }));
                test_cases.Add(new Tuple<int, IList<int>>(-1, new[] { 0, 2, 4, 6 }));
                test_cases.Add(new Tuple<int, IList<int>>(-1, new[] { -2, 0, 2, 4 }));
                test_cases.Add(new Tuple<int, IList<int>>(-1, new[] { -4, -2, 0, 2 }));
                test_cases.Add(new Tuple<int, IList<int>>(-1, new[] { -6, -4, -2, 0 }));

                message = test_case => string.Format("Expected index of <{0}>, but was <{1}> for collection {2}", "{0}", "{1}", test_case.Item2.ToStringOfItems());
            };

            It should_return_the_index_of_the_item = () =>
                test_cases.each(x => sut.find(item_to_find, x.Item2).ShouldEqual(x.Item1, message(x)));

            static int item_to_find;
            static List<Tuple<int, IList<int>>> test_cases;
            static Func<Tuple<int, IList<int>>, string> message;
        }
    }

    public class RecursiveBinaryChop : IFindAnItem
    {
        public int find(int itemToFind, IList<int> collection)
        {
            if (!collection.Any())
                return -1;

            switch(collection[collection.Count / 2].CompareTo(itemToFind))
            {
                case -1:
                    return find(itemToFind, collection.Take(collection.Count / 2 - 1).ToList());
                case 1:
                    return find(itemToFind, collection.Skip(collection.Count / 2 + 1).ToList());
                default:
                    return collection.Count / 2;
            }

        }
    }

    public static class SpecExtensions
    {
        public static void ShouldEqual<T>(this T actual, T expected, string message)
        {
            try
            {
                actual.ShouldEqual(expected);
            }
            catch (SpecificationException ex)
            {
                throw new SpecificationException(string.Format(message, expected, actual), ex);
            }
        }

        public static string ToStringOfItems<T>(this IEnumerable<T> list)
        {
            return string.Format("[{0}]", list.Aggregate("", (x, y) => x + y.ToString() + ", "));
        }
    }
}
