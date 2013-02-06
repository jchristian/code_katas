using System.Collections.Generic;
using System.Linq;

namespace console
{
    public class SimpleHashTable<TKey, TValue> : IHashTable<TKey, TValue>
    {
        IHashGenerator<TKey> hash_generator;
        IList<KeyValuePair<TKey, TValue>>[] values;

        public SimpleHashTable(IHashGenerator<TKey> hash_generator) : this(hash_generator, 100) { }
        public SimpleHashTable(IHashGenerator<TKey> hash_generator, int size)
        {
            this.hash_generator = hash_generator;
            values = new IList<KeyValuePair<TKey, TValue>>[size];
        }

        public virtual TValue this[TKey key]
        {
            get { return GetValue(key); }
            set { SetValue(key, value); }
        }

        void SetValue(TKey key, TValue value)
        {
            var hash = hash_generator.GetHash(key);

            if (values.GetModuloValue(hash) == null)
                values.SetModuloValue(hash, new List<KeyValuePair<TKey, TValue>>());

            if (values.GetModuloValue(hash).Any(x => x.Key.Equals(key)))
                values.GetModuloValue(hash).Remove(values.GetModuloValue(hash).Single(x => x.Key.Equals(key)));

            values.GetModuloValue(hash).Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        TValue GetValue(TKey key)
        {
            var hash = hash_generator.GetHash(key);
            var hash_matches = values.GetModuloValue(hash);

            if (hash_matches == null)
                throw new KeyNotFoundException(string.Format("The key <{0}> could not be found", key));

            if (!hash_matches.Any(x => x.Key.Equals(key)))
                throw new KeyNotFoundException(string.Format("The key <{0}> could not be found", key));

            return hash_matches.Single(x => x.Key.Equals(key)).Value;
        }
    }
}