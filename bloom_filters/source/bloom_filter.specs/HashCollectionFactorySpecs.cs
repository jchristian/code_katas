using System.Collections.Generic;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using prep.bf;

namespace prep.specs
{
  public class HashCollectionFactorySpecs
  {
    public abstract class concern : Observes<ICreateManyHashes,HashCollectionFactory>
    {
    }

    [Subject(typeof(HashCollectionFactory))]
    public class getting_the_hashes_for_a_word : concern
    {
      Establish c = () =>
      {
        var hash_creator = fake.an<ICreateAnSingleHash>();
        depends.on<IEnumerable<ICreateAnSingleHash>>(new[] { hash_creator });

        hash_creator.setup(x => x.create_for(word)).Return(hash);
      };

      Because of = () =>
        the_generated_hashes = sut.get_hashes_for(word);

      It should_return_the_set_of_hashes_created_by_the_hash_generators = () =>
        the_generated_hashes.ShouldContainOnly(new[] {hash});

      static IEnumerable<int> the_generated_hashes;
      static int hash;
      static string word;
    }
  }
}