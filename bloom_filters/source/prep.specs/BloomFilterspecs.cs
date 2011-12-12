using System.Collections.Generic;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using prep.bf;

namespace prep.specs
{
    public class BloomFilterSpecs
    {
        public abstract class concern : Observes<ICheckIfIContainHashes,
                                            BloomFilter>
        {
        }

        [Subject(typeof(BloomFilter))]
        public class when_checking_to_see_if_the_hashes_are_contained : concern
        {
            public class and_all_the_hashes_are_contained
            {
                Establish c = () =>
                {
                    the_hashes = new[] {1};

                    depends.on<IEnumerable<int>>(new[] {1});
                };

                Because of = () =>
                    are_all_the_hashes_contained = sut.contains(the_hashes);

                It should_return_true = () =>
                    are_all_the_hashes_contained.ShouldBeTrue();

                static bool are_all_the_hashes_contained;
                static IEnumerable<int> the_hashes;
            }

            public class and_a_hash_is_not_contained
            {
                Establish c = () =>
                {
                    the_hashes = new[] { 1 };

                    depends.on<IEnumerable<int>>(new[] { 2 });
                };

                Because of = () =>
                    are_all_the_hashes_contained = sut.contains(the_hashes);

                It should_return_false = () =>
                    are_all_the_hashes_contained.ShouldBeFalse();

                static bool are_all_the_hashes_contained;
                static IEnumerable<int> the_hashes;
            }
        }
    }
}