using System.Collections.Generic;

namespace prep.bf
{
    public class SpellChecker
    {
        ICheckIfIContainHashes bloom_filter;
        ICreateManyHashes many_hash_factory;

        public SpellChecker(ICheckIfIContainHashes bloom_filter, ICreateManyHashes many_hash_factory)
        {
            this.bloom_filter = bloom_filter;
            this.many_hash_factory = many_hash_factory;
        }

        public bool is_valid_word(string word)
        {
            return bloom_filter.contains(many_hash_factory.get_hashes_for(word));
        }
    }

    public interface ICheckIfIContainHashes
    {
        void add(int the_hash);
        bool contains(IEnumerable<int> hashes);
    }

    public interface ICreateManyHashes
    {
        IEnumerable<int> get_hashes_for(string word);
    }
}