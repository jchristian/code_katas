using System;
using System.Collections.Generic;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.moq;
using Machine.Specifications;
using source.class_base_recursion;

namespace source.specs
{
    public class DecoratorBinaryChopSpecs
    {
        public abstract class concern : Observes<IFindAnItem,
                                            DecoratorBinaryChop> { }

        [Subject(typeof(DecoratorBinaryChop))]
        public class when_finding_the_item : concern
        {
            Establish c = () =>
            {
                var setup = new BinaryChopTestSetup();

                item_to_find = setup.item_to_find;
                test_cases = setup.test_cases;
                message = setup.message;

                actual_sut = new LazyDecoratorBinaryChop();
            };

            It should_return_the_index_of_the_item = () =>
                test_cases.each(x => actual_sut.find(item_to_find, x.Item2).ShouldEqual(x.Item1, message(x)));

            static int item_to_find;
            static List<Tuple<int, IList<int>>> test_cases;
            static Func<Tuple<int, IList<int>>, string> message;
            static LazyDecoratorBinaryChop actual_sut;
        }
    }
}