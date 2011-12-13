using System.Collections.Generic;

namespace prep.bf
{
    public interface ICreateManyHashes
    {
        IEnumerable<int> get_hashes_for(string word);
    }
}