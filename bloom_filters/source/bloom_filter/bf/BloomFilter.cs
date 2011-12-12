using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace prep.bf
{
    public class BloomFilter : ICheckIfIContainHashes
    {
        const int filter_lenght = 10000;
        BitArray filter;

        public BloomFilter(IEnumerable<int> hashes)
        {
            initialize_filter(hashes);
        }

        public bool contains(IEnumerable<int> hashes)
        {
            return hashes.All(x => filter[x % 10000]);
        }

        void initialize_filter(IEnumerable<int> hashes)
        {
            filter = new BitArray(filter_lenght);

            foreach (var hash in hashes)
                filter[hash % filter_lenght] = true;
        }
    }
}