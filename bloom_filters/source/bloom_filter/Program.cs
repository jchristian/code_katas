using System;
using System.Linq;
using bloom_filter.bf;
using developwithpassion.specifications.extensions;
using prep.bf;

namespace prep
{
    class Program
    {
        static void Main(string[] args)
        {
            var spellChecker = new SpellChecker(new EnglishDictionary(), new BloomFilter(), new HashCollectionFactory(new HashFactoryCollection()));

            Console.WriteLine("Enter a sentence to be spell checked: ");
            var inputSentence = Console.ReadLine();

            var words_not_in_dictionary = inputSentence.Split(' ').Where(word => !spellChecker.is_word_in_dictionary(word.ToLowerInvariant()));

            Console.WriteLine("Here are the words you may have misspelled: ");
            words_not_in_dictionary.each(Console.WriteLine);
            Console.ReadLine();
        }
    }
}