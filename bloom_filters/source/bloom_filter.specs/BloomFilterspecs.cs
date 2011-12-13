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
                    the_hash = 1;
                    the_hashes = new[] {the_hash};
                };

                Because of = () =>
                {
                    sut.add(the_hash);
                    are_all_the_hashes_contained = sut.contains(the_hashes);
                };

                It should_return_true = () =>
                    are_all_the_hashes_contained.ShouldBeTrue();

                static bool are_all_the_hashes_contained;
                static int the_hash;
                static IEnumerable<int> the_hashes;
            }

            public class and_a_hash_is_not_contained
            {
                Establish c = () =>
                {
                    the_first_hash = 1;
                    the_second_hash = 2;
                    the_hashes = new[] { the_first_hash, the_second_hash };
                };

                Because of = () =>
                {
                    sut.add(the_first_hash);
                    are_all_the_hashes_contained = sut.contains(the_hashes);
                };

                It should_return_false = () =>
                    are_all_the_hashes_contained.ShouldBeFalse();

                static bool are_all_the_hashes_contained;
                static int the_first_hash;
                static int the_second_hash;
                static IEnumerable<int> the_hashes;
            }
        }
    }
}