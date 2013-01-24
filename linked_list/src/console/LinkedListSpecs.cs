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

        [Subject(typeof(LinkedList<int>))]
        public class when_removing_an_item_from_the_middle_of_the_list : concern
        {
            Establish c = () =>
            {
                sut_setup.run(x => x.Add(1));
                sut_setup.run(x => x.Add(2));
                sut_setup.run(x => x.Add(3));
            };

            Because of = () =>
                sut.Remove(2);

            It should_remove_the_item = () =>
            {
                sut.Count.ShouldEqual(2);
                sut[0].ShouldEqual(1);
                sut[1].ShouldEqual(3);
            };
        }

        [Subject(typeof(LinkedList<int>))]
        public class when_removing_an_item_from_the_beginning_of_the_list : concern
        {
            Establish c = () =>
            {
                sut_setup.run(x => x.Add(1));
                sut_setup.run(x => x.Add(2));
                sut_setup.run(x => x.Add(3));
            };

            Because of = () =>
                sut.Remove(1);

            It should_remove_the_item = () =>
            {
                sut.Count.ShouldEqual(2);
                sut[0].ShouldEqual(2);
                sut[1].ShouldEqual(3);
            };
        }

        [Subject(typeof(LinkedList<int>))]
        public class when_removing_an_item_that_does_not_exist : concern
        {
            Establish c = () =>
            {
                sut_setup.run(x => x.Add(1));
                sut_setup.run(x => x.Add(2));
                sut_setup.run(x => x.Add(3));
            };

            Because of = () =>
                was_removal_successful = sut.Remove(4);

            It should_return_false = () =>
                was_removal_successful.ShouldEqual(false);
            
            static bool was_removal_successful;
        }

        [Subject(typeof(LinkedList<int>))]
        public class when_removing_an_item_at_an_index_location : concern
        {
            Establish c = () =>
            {
                sut_setup.run(x => x.Add(1));
                sut_setup.run(x => x.Add(2));
                sut_setup.run(x => x.Add(3));
            };

            Because of = () =>
                sut.RemoveAt(1);

            It should_remove_the_item = () =>
            {
                sut.Count.ShouldEqual(2);
                sut[0].ShouldEqual(1);
                sut[1].ShouldEqual(3);
            };
        }

        [Subject(typeof(LinkedList<int>))]
        public class when_clearing_the_list : concern
        {
            Establish c = () =>
            {
                sut_setup.run(x => x.Add(1));
                sut_setup.run(x => x.Add(2));
                sut_setup.run(x => x.Add(3));
            };

            Because of = () =>
                sut.Clear();

            It should_remove_all_the_items = () =>
            {
                sut.Count.ShouldEqual(0);
                sut.IndexOf(1).ShouldEqual(-1);
                sut.IndexOf(2).ShouldEqual(-1);
                sut.IndexOf(3).ShouldEqual(-1);
            };
        }

        [Subject(typeof(LinkedList<int>))]
        public class when_copying_an_array_of_values : concern
        {
            Establish c = () =>
            {
                sut_setup.run(x => x.Add(1));
                sut_setup.run(x => x.Add(2));
                sut_setup.run(x => x.Add(3));
            };

            Because of = () =>
                sut.CopyTo(new[] { 4, 5, 6 }, 1);

            It should_copy_all_the_items_into_the_list = () =>
            {
                sut.Count.ShouldEqual(6);
                sut.IndexOf(4).ShouldEqual(1);
                sut.IndexOf(5).ShouldEqual(2);
                sut.IndexOf(6).ShouldEqual(3);
            };
        }

        [Subject(typeof(LinkedList<int>))]
        public class when_enumerating_through_the_list : concern
        {
            Establish c = () =>
            {
                sut_setup.run(x => x.Add(1));
                sut_setup.run(x => x.Add(2));
                sut_setup.run(x => x.Add(3));
            };

            Because of = () =>
                enumerator = sut.GetEnumerator();

            It should_copy_all_the_items_into_the_list = () =>
            {
                enumerator.MoveNext().ShouldEqual(true);
                enumerator.Current.ShouldEqual(1);
                enumerator.MoveNext().ShouldEqual(true);
                enumerator.Current.ShouldEqual(2);
                enumerator.MoveNext().ShouldEqual(true);
                enumerator.Current.ShouldEqual(3);
                enumerator.MoveNext().ShouldEqual(false);
            };

            static IEnumerator<int> enumerator;
        }
    }
}
