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
          depends_on<>();
        };

        Because of = () =>
          are_all_the_hashes_contained = sut.contains(the_hashes);

        It should_return_true = () =>
          are_all_the_hashes_contained.ShouldBeTrue();

        static bool are_all_the_hashes_contained;
        static IEnumerable<int> the_hashes;
      }
    }
  }
}
