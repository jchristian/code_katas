using System.Collections.Generic;
using developwithpassion.specifications.extensions;

namespace prep.bf
{
    public class SpellChecker
    {
        ICheckIfIContainHashes bloom_filter;
        ICreateManyHashes many_hash_factory;

        public SpellChecker(IEnumerable<string> dictionary, ICheckIfIContainHashes bloom_filter, ICreateManyHashes many_hash_factory)
        {
            this.bloom_filter = bloom_filter;
            this.many_hash_factory = many_hash_factory;

            dictionary.each(word => many_hash_factory.get_hashes_for(word).each(bloom_filter.add));
        }

        public bool is_word_in_dictionary(string word)
        {
            return bloom_filter.contains(many_hash_factory.get_hashes_for(word));
        }
    }
}