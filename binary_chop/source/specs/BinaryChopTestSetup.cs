using System;
using System.Collections.Generic;

namespace source.specs
{
    public class BinaryChopTestSetup
    {
        public BinaryChopTestSetup()
        {
            item_to_find = 1;

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
        }

        public int item_to_find;
        public List<Tuple<int, IList<int>>> test_cases;
        public Func<Tuple<int, IList<int>>, string> message;
    }
}