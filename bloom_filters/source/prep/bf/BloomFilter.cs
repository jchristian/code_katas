using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications.Utility;

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

        void initialize_filter(IEnumerable<int> hashes)
        {
            filter = new BitArray(filter_lenght);
            hashes.Each(x => filter[x%filter_lenght] = true);
        }

        public bool contains(IEnumerable<int> hashes)
        {
            return hashes.All(x => filter[x%10000]);
        }
    }
}