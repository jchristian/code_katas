using System.Linq;
using console;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.moq;
using Machine.Specifications;

namespace tests
{
    public class BlockBoardSpecs
    {
        public abstract class concern : Observes<BlockBoard>
        { }

        public abstract class and_the_board_is_empty : Observes<BlockBoard>
        {
            Establish c = () =>
            {
                sut_factory.create_using(() => new BlockBoard());
            };
        }

        [Subject(typeof(BlockBoard))]
        public class when_creating_a_board : and_the_board_is_empty
        {
            It should_have_no_rows = () =>
                sut.Rows.ShouldBeEmpty();
        }

        [Subject(typeof(BlockBoard))]
        public class when_adding_a_block_to_an_empty_board_on_the_first_row : and_the_board_is_empty
        {
            Because of = () =>
                sut.AddBlock(0, 0);

            It should_have_a_single_row = () =>
                sut.Rows.Count().ShouldEqual(1);

            It should_have_a_block_in_the_first_cell = () =>
                sut.Rows.First().Cells.First().ShouldEqual(true);
        }

        [Subject(typeof(BlockBoard))]
        public class when_adding_a_block_to_an_empty_board_on_the_second_row : and_the_board_is_empty
        {
            Because of = () =>
                sut.AddBlock(0, 1);

            It should_have_a_two_rows = () =>
                sut.Rows.Count().ShouldEqual(2);

            It should_have_a_block_in_the_first_cell = () =>
                sut.Rows.ElementAt(1).Cells.First().ShouldEqual(true);
        }

        [Subject(typeof(BlockBoard))]
        public class when_applying_gravity : and_the_board_is_empty
        {
            Because of = () =>
                spec.catch_exception(() => sut.ApplyGravity());

            It should_not_throw_an_exception = () =>
                spec.exception_thrown.ShouldBeNull();
        }

        [Subject(typeof(BlockBoard))]
        public class when_applying_gravity_to_a_block_that_will_fall : concern
        {
            Establish c = () =>
            {
                sut_factory.create_using(() => new BlockBoard(new[]
                                                 {
                                                     new[] { true }.ToList(),
                                                     new[] { false }.ToList()
                                                 }.Reverse()));
            };

            Because of = () =>
                sut.ApplyGravity();

            It should_have_a_block_that_has_fallen = () =>
                sut.Rows.First().Cells.First().ShouldEqual(true);
        }

        [Subject(typeof(BlockBoard))]
        public class when_shifting_a_row_of_blocks_right : concern
        {
            Establish c = () =>
            {
                sut_factory.create_using(() => new BlockBoard(new[]
                                                 {
                                                     new[] { true, true, true }.ToList()
                                                 }.Reverse()));
            };

            Because of = () =>
                sut.ShiftRowRight(0);

            It should_shift_the_blocks_right_by_one_unit = () =>
                sut.Rows.First().Cells.ShouldContainOnlyInOrder(new[] { false, true, true, true });
        }

        [Subject(typeof(BlockBoard))]
        public class when_shifting_a_row_of_blocks_right_and_a_block_should_fall: concern
        {
            Establish c = () =>
            {
                sut_factory.create_using(() => new BlockBoard(new[]
                                                 {
                                                     new[] { true }.ToList(),
                                                     new[] { true }.ToList()
                                                 }.Reverse()));
            };

            Because of = () =>
            {
                sut.ShiftRowRight(0);
                sut.ApplyGravity();
            };

            It should_shift_the_blocks_right_by_one_unit = () =>
            {
                sut.Rows.First().Cells.ShouldContainOnlyInOrder(new[] { true, true });
            };
        }
    }
}
