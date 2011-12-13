using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace prep.bf
{
    public class BloomFilter : ICheckIfIContainHashes
    {
        const int filter_length = 10000;
        BitArray filter;

        public BloomFilter()
        {
            filter = new BitArray(filter_length);
        }

        public void add(int the_hash)
        {
            filter[the_hash % filter_length] = true;
        }

        public bool contains(IEnumerable<int> hashes)
        {
            return hashes.All(x => filter[x % 10000]);
        }
    }
}