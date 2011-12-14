using System.Collections;
using System.Collections.Generic;

namespace bloom_filter.bf
{
    public class EnglishDictionary : IEnumerable<string>
    {
        public IEnumerator<string> GetEnumerator()
        {
            yield return "the";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}