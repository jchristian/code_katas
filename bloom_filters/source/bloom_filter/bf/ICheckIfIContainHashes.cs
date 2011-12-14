using System.Collections.Generic;

namespace prep.bf
{
    public interface ICheckIfIContainHashes
    {
        void add(int the_hash);
        bool contains(IEnumerable<int> hashes);
    }
}