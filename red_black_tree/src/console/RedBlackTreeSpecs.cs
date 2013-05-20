using System.Collections.Generic;
using Machine.Specifications;
using developwithpassion.specifications.moq;

namespace console
{
    public class RedBlackTreeSpecs
    {
        public abstract class concern : Observes<IDictionary<int, string>,
                                            RedBlackTree<int, string>>
        {
            Establish c = () => sut_factory.create_using(() => new RedBlackTree<int, string>());
        }

        [Subject(typeof(RedBlackTree<int, int>))]
        public class when_adding_a_value : concern
        {
            public class and_values_have_already_been_added
            {
                Establish c = () =>
                {
                    key = 1;
                    value = "1";

                    sut_setup.run(x => x.Add(2, "2"));
                };

                Because of = () =>
                    sut.Add(key, value);

                It should_be_able_to_retrieve_the_value_using_the_key = () =>
                    sut[key].ShouldEqual(value);

                static int key;
                static string value;
            }
        }
    }
}
