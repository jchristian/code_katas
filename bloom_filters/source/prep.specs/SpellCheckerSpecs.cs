using System.Collections.Generic;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using prep.bf;

namespace prep.specs
{
  public class SpellCheckerSpecs
  {
    public abstract class concern : Observes<SpellChecker>
    {
    }

    [Subject(typeof(SpellChecker))]
    public class when_determining_if_a_word_is_valid : concern
    {
      public class and_the_words_hashes_are_in_the_bloom_filter
      {
        Establish c = () =>
        {
          word = "test";
          var hash = 10;

          var hash_creator = depends.on<ICreateManyHashes>();
          var bloom_filter = depends.on<ICheckIfIContainHashes>();
          var hashes_for_the_word = new[] { hash };

          hash_creator.setup(x => x.get_hashes_for(word)).Return(hashes_for_the_word);
          bloom_filter.setup(x => x.contains(hashes_for_the_word)).Return(true);
        };

        Because of = () =>
          does_the_word_exist = sut.is_valid_word(word);

        It should_return_true = () =>
          does_the_word_exist.ShouldBeTrue();

        static bool does_the_word_exist;
        static string word;
      }

      public class and_the_words_hashes_are_not_in_the_bloom_filter
      {
        Establish c = () =>
        {
          word = "test";
          var hash = 10;

          var hash_creator = depends.on<ICreateManyHashes>();
          var bloom_filter = depends.on<ICheckIfIContainHashes>();
          depends.on<IEnumerable<string>>(new[] { word });

          var hashes_for_the_word = new[] { hash };

          hash_creator.setup(x => x.get_hashes_for(word)).Return(hashes_for_the_word);
          bloom_filter.setup(x => x.contains(hashes_for_the_word)).Return(false);
        };

        Because of = () =>
          does_the_word_exist = sut.is_valid_word(word);

        It should_return_false = () =>
          does_the_word_exist.ShouldBeFalse();

        static bool does_the_word_exist;
        static string word;
      }
    }
  }
}