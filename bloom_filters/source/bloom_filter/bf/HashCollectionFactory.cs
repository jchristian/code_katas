using System.Collections.Generic;
using System.Linq;

namespace prep.bf
{
  public class HashCollectionFactory : ICreateManyHashes
  {
    IEnumerable<ICreateAnSingleHash> hash_creators;

    public HashCollectionFactory(IEnumerable<ICreateAnSingleHash> hash_creators)
    {
      this.hash_creators = hash_creators;
    }

    public IEnumerable<int> get_hashes_for(string word)
    {
      return hash_creators.Select(x => x.create_for(word));
    }
  }
}