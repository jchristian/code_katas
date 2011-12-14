using System.Collections;
using System.Collections.Generic;
using prep.bf;

namespace bloom_filter.bf
{
    public class HashFactoryCollection : IEnumerable<ICreateAnSingleHash>
    {
        public IEnumerator<ICreateAnSingleHash> GetEnumerator()
        {
            yield return new FrameworkHashFactory();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}