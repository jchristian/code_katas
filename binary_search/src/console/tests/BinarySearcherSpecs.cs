using System.Linq;
using Machine.Specifications;
using developwithpassion.specifications.moq;

namespace console.tests
{
    public class BinarySearcherSpecs
    {
        public abstract class concern : Observes<BinarySearcher> {}

        [Subject(typeof(BinarySearcher))]
        public class when_searching_for_a_key : concern
        {
            public class and_the_list_is_empty
            {
                Because of = () =>
                    index = sut.Find(Enumerable.Empty<int>(), 0);

                It should_return_negative_one = () =>
                    index.ShouldEqual(-1);

                static int index;
            }

            public class and_the_key_does_not_exist
            {
                Because of = () =>
                    index = sut.Find(new[] { 1, 2, 3 }, 0);

                It should_return_negative_one = () =>
                    index.ShouldEqual(-1);

                static int index;
            }

            public class and_the_key_does_not_exist_but_is_within_the_boundaries_of_the_list
            {
                Because of = () =>
                    index = sut.Find(new[] { 1, 2, 4, 5 }, 3);

                It should_return_negative_one = () =>
                    index.ShouldEqual(-1);

                static int index;
            }

            public class and_the_key_is_in_the_list
            {
                Because of = () =>
                    index = sut.Find(new[] { -10, 2, 17, 21, 24, 34, 35, 101 }, 34);

                It should_return_the_index_of_the_middle_of_the_list = () =>
                    index.ShouldEqual(5);

                static int index;
            }

            public class and_the_key_is_at_the_beginning_of_the_list
            {
                Because of = () =>
                    index = sut.Find(new[] { 1, 2, 3, 4, 5 }, 1);

                It should_return_zero = () =>
                    index.ShouldEqual(0);

                static int index;
            }

            public class and_the_key_is_at_the_end_of_the_list
            {
                Because of = () =>
                    index = sut.Find(new[] { 1, 2, 3, 4, 5 }, 5);

                It should_return_the_index_of_the_end_of_the_list = () =>
                    index.ShouldEqual(4);

                static int index;
            }

            public class and_the_key_is_in_the_middle_of_the_list_and_there_are_duplicate_key_values
            {
                Because of = () =>
                    index = sut.Find(new[] { 1, 2, 3, 3, 3, 3, 3, 3, 3, 4, 5 }, 3);

                It should_return_the_index_of_the_first_instance_of_the_key = () =>
                    index.ShouldEqual(2);

                static int index;
            }

            public class and_the_key_is_at_the_beginning_of_the_list_and_there_are_duplicate_key_values
            {
                Because of = () =>
                    index = sut.Find(new[] { 1, 1, 1, 2, 3, 4, 5 }, 1);

                It should_return_the_index_of_the_first_instance_of_the_key = () =>
                    index.ShouldEqual(0);

                static int index;
            }

            public class and_the_key_is_at_the_end_of_the_list_and_there_are_duplicate_key_values
            {
                Because of = () =>
                    index = sut.Find(new[] { 1, 2, 3, 4, 5, 5, 5 }, 5);

                It should_return_the_index_of_the_first_instance_of_the_key = () =>
                    index.ShouldEqual(4);

                static int index;
            }
        }
    }
}
