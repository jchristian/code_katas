using System;
using System.Collections.Generic;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.moq;
using Machine.Specifications;
using source.recursive;

namespace source.specs
{
    public class RecursiveBinaryChopSpecs
    {
        public abstract class concern : Observes<IFindAnItem,
                                            RecursiveBinaryChop> { }

        [Subject(typeof(RecursiveBinaryChop))]
        public class when_finding_the_item : concern
        {
            Establish c = () =>
            {
                var setup = new BinaryChopTestSetup();

                item_to_find = setup.item_to_find;
                test_cases = setup.test_cases;
                message = setup.message;
            };

            It should_return_the_index_of_the_item = () =>
                test_cases.each(x => sut.find(item_to_find, x.Item2).ShouldEqual(x.Item1, message(x)));

            static int item_to_find;
            static List<Tuple<int, IList<int>>> test_cases;
            static Func<Tuple<int, IList<int>>, string> message;
        }
    }
}
