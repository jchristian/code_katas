using System;
using System.Collections.Generic;
using Machine.Specifications;
using developwithpassion.specifications.nsubstitue;

namespace console
{
    public class LinkedListSpecs
    {
        public abstract class concern : Observes<IList<int>,
                                            LinkedList<int>> { }

        [Subject(typeof(LinkedList<int>))]
        public class when_adding_to_an_empty_list : concern
        {
            Establish c = () =>
                added_item = 1;

            Because of = () =>
                sut.Add(added_item);

            It should_add_the_item_to_the_list = () =>
                sut[0].ShouldEqual(added_item);

            static int added_item;
        }

        [Subject(typeof(LinkedList<int>))]
        public class when_adding_to_a_populated_list : concern
        {
            Establish c = () =>
            {
                added_item = 1;
                sut_setup.run(x => x.Add(2));
                sut_setup.run(x => x.Add(3));
            };

            Because of = () =>
                sut.Add(added_item);

            It should_add_the_item_to_the_end_of_the_list = () =>
                sut[2].ShouldEqual(added_item);

            static int added_item;
        }

        [Subject(typeof(LinkedList<int>))]
        public class when_inserting_an_item_into_the_middle_of_a_list : concern
        {
            Establish c = () =>
            {
                added_item = 1;
                previous_item_at_inserted_index = 3;
                sut_setup.run(x => x.Add(2));
                sut_setup.run(x => x.Add(previous_item_at_inserted_index));
                sut_setup.run(x => x.Add(4));
            };

            Because of = () =>
                sut.Insert(1, added_item);

            It should_insert_the_item_into_the_correct_position = () =>
                sut[1].ShouldEqual(added_item);

            It should_move_the_item_previously_in_the_inserted_index_forward = () =>
                sut[2].ShouldEqual(previous_item_at_inserted_index);

            static int added_item;
            static int previous_item_at_inserted_index;
        }

        [Subject(typeof(LinkedList<int>))]
        public class when_inserting_an_item_to_the_end_of_the_list : concern
        {
            Establish c = () =>
            {
                added_item = 1;
                sut_setup.run(x => x.Add(2));
                sut_setup.run(x => x.Add(3));
            };

            Because of = () =>
                sut.Insert(2, added_item);

            It should_insert_the_item_into_the_correct_position = () =>
                sut[2].ShouldEqual(added_item);

            static int added_item;
        }

        [Subject(typeof(LinkedList<int>))]
        public class when_inserting_an_item_outside_the_lists_size : concern
        {
            Establish c = () =>
            {
                added_item = 1;
                sut_setup.run(x => x.Add(2));
                sut_setup.run(x => x.Add(3));
            };

            Because of = () =>
                spec.catch_exception(() => sut.Insert(3, added_item));

            It should_throw_an_argument_exception_with_an_argument_of_index = () =>
            {
                spec.exception_thrown.ShouldBe(typeof(ArgumentException));
                ((ArgumentException)spec.exception_thrown).ParamName.ShouldEqual("index");
            };

            static int added_item;
        }
    }
}
