using System.Collections.Generic;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using prep.bf;

namespace prep.specs
{
    public class SpellCheckerSpecs
    {
        public abstract class concern : Observes<SpellChecker> {}

        [Subject(typeof(SpellChecker))]
        public class when_determining_if_a_word_is_valid : concern
        {
            public class and_the_words_hashes_are_in_the_bloom_filter
            {
                static bool does_the_word_exist;
                static string word;

                Establish c = () =>
                {
                    word = "test";
                    var hash = 1;

                    var hash_creator = depends.on<ICreateManyHashes>();
                    var bloom_filter = depends.on<ICheckIfIContainHashes>();
                    var hashes_for_the_word = new[] { hash };

                    hash_creator.setup(x => x.get_hashes_for(word)).Return(hashes_for_the_word);
                    bloom_filter.setup(x => x.contains(hashes_for_the_word)).Return(true);
                };

                Because of = () =>
                    does_the_word_exist = sut.is_word_in_dictionary(word);

                It should_return_true = () =>
                    does_the_word_exist.ShouldBeTrue();
            }

            public class and_the_words_hashes_are_not_in_the_bloom_filter
            {
                static bool does_the_word_exist;
                static string word;

                Establish c = () =>
                {
                    word = "test";
                    var hash = 1;

                    var hash_creator = depends.on<ICreateManyHashes>();
                    var bloom_filter = depends.on<ICheckIfIContainHashes>();
                    var hashes_for_the_word = new[] { hash };

                    hash_creator.setup(x => x.get_hashes_for(word)).Return(hashes_for_the_word);
                    bloom_filter.setup(x => x.contains(hashes_for_the_word)).Return(false);
                };

                Because of = () =>
                    does_the_word_exist = sut.is_word_in_dictionary(word);

                It should_return_false = () =>
                    does_the_word_exist.ShouldBeFalse();
            }
        }

        [Subject(typeof(SpellChecker))]
        public class when_creating_the_spell_checker : concern
        {
            Establish c = () =>
            {
                var first_word = "first word";
                var second_word = "second word";
                first_set_of_hashes = new[] { 1 };
                second_set_of_hashes = new[] { 2 };

                var hash_creator = depends.on<ICreateManyHashes>();
                bloom_filter = depends.on<ICheckIfIContainHashes>();
                depends.on<IEnumerable<string>>(new[] { first_word, second_word });

                hash_creator.setup(x => x.get_hashes_for(first_word)).Return(first_set_of_hashes);
                hash_creator.setup(x => x.get_hashes_for(second_word)).Return(second_set_of_hashes);
            };

            It should_add_the_hashes_for_the_first_word_in_the_dictionary_to_the_bloom_filter = () =>
                first_set_of_hashes.each(hash => bloom_filter.received(x => x.add(hash)));

            It should_add_the_hashes_for_the_second_word_in_the_dictionary_to_the_bloom_filter = () =>
                second_set_of_hashes.each(hash => bloom_filter.received(x => x.add(hash)));

            static ICheckIfIContainHashes bloom_filter;
            static int[] first_set_of_hashes;
            static int[] second_set_of_hashes;
        }
    }
}