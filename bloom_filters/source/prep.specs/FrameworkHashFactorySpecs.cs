using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using prep.bf;

namespace prep.specs
{
  public class FrameworkHashFactorySpecs
  {
    public abstract class concern : Observes<ICreateAnSingleHash, FrameworkHashFactory>
    {
    }

    [Subject(typeof(FrameworkHashFactory))]
    public class when_generating_a_hash_for_the_word : concern
    {
      Establish c = () =>
      {
        word = "test";
        hash = word.GetHashCode();
      };

      Because of = () =>
        the_generated_hash = sut.create_for(word);

      It should_return_the_hash_from_the_framework = () =>
        the_generated_hash.ShouldEqual(hash);

      static int the_generated_hash;
      static int hash;
      static string word;
    }
  }
}