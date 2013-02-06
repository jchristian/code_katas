using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Machine.Specifications;
using Microsoft.VisualStudio.Shell.Interop;
using developwithpassion.specifications.moq;

namespace console.tests
{
    public class HastTableSpecs
    {
        public abstract class concern : Observes<IHashTable<int, string>>
        {
            Establish c = () =>
            {
                sut_factory.create_using(() => new SimpleHashTable<int, string>(new GenericHashGenerator<int>(), 10000));
            };

            public static Action<IEnumerable<Tuple<int, string>>, IHashTable<int, string>> act = (maps, sut) =>
            {
                foreach (var map in maps)
                    sut[map.Item1] = map.Item2;
            };

            public static Action<IEnumerable<Tuple<int, string>>, IHashTable<int, string>> assert = (maps, sut) =>
            {
                var exceptions = new List<Tuple<int, string, string>>();
                foreach (var map in maps)
                    if (sut[map.Item1] != map.Item2)
                        exceptions.Add(new Tuple<int, string, string>(map.Item1, map.Item2, sut[map.Item1]));

                if (exceptions.Count > 0)
                    throw new Exception(string.Format(exceptions.Aggregate("The following maps did not map correctly:", (x, y) => x + "{0}" + string.Format("Key <{0}> Mapped to <{1}> but should have mapped to <{2}>", y.Item1, y.Item3, y.Item2)), Environment.NewLine));
            };

            public static Action<int, IEnumerable<Tuple<int, string>>, IHashTable<int, string>> time_x_lookups = (number_of_lookups, maps, sut) =>
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var random = new Random();
                string value;
                foreach (var lookup in Enumerable.Range(0, number_of_lookups))
                    value = sut[maps.ElementAt(random.Next(maps.Count() - 1)).Item1];

                stopwatch.Stop();
                Debug.WriteLine("Ran {0} lookups on {1} values in {2}ms", number_of_lookups, maps.Count(), stopwatch.ElapsedMilliseconds);
            };
        }

        [Subject(typeof(SimpleHashTable<int, string>))]
        public class when_hashing_multiple_values : concern
        {
            Establish c = () =>
            {
                maps = new[]
                       {
                           new Tuple<int, string>(1, "One"),
                           new Tuple<int, string>(2, "Two"),
                           new Tuple<int, string>(22, "Three"),
                           new Tuple<int, string>(101, "One Oh One"),
                           new Tuple<int, string>(1100203921, "A lot"),
                           new Tuple<int, string>(-1100203921, "A little"),
                           new Tuple<int, string>(11, "Eleven"),
                           new Tuple<int, string>(-11, "Negative Eleven"),
                           new Tuple<int, string>(45, "Fourty Five"),
                           new Tuple<int, string>(3, "Three"),
                           new Tuple<int, string>(7, "Seven"),
                           new Tuple<int, string>(12, "Twelve"),
                       };
            };

            Because of = () => act(maps, sut);

            It should_return_the_correct_values = () => assert(maps, sut);

            static IEnumerable<Tuple<int, string>> maps;
        }

        [Subject(typeof(SimpleHashTable<int, string>))]
        public class when_hashing_duplicate_keys : concern
        {
            Establish c = () =>
            {
                maps = new[]
                       {
                           new Tuple<int, string>(1, "One"),
                           new Tuple<int, string>(101, "Two"),
                           new Tuple<int, string>(1, "Three")
                       };
            };

            Because of = () => act(maps, sut);

            It should_return_the_correct_values = () => assert(maps.Skip(1), sut);

            static IEnumerable<Tuple<int, string>> maps;
        }

        [Subject(typeof(SimpleHashTable<int, string>))]
        public class when_retrieving_a_key_not_present : concern
        {
            Establish c = () =>
            {
                maps = new[]
                       {
                           new Tuple<int, string>(1, "One"),
                           new Tuple<int, string>(10001, "Two")
                       };
            };

            Because of = () =>
            {
                act(maps, sut);
                spec.catch_exception(() => { var value = sut[20001]; });
            };

            It should_throw_a_key_not_found_exception = () =>
                spec.exception_thrown.ShouldBe(typeof(KeyNotFoundException));

            static IEnumerable<Tuple<int, string>> maps;
        }

        [Subject(typeof(SimpleHashTable<int, string>))]
        public class when_hashing_a_ten_thousand_values : concern
        {
            Establish c = () =>
            {
                var rand = new Random();
                maps = Enumerable.Range(0, 10000).Select(x => new Tuple<int, string>(rand.Next(int.MaxValue), rand.Next(int.MaxValue).ToString())).ToList();
            };

            Because of = () => act(maps, sut);

            It print = () => time_x_lookups(100000, maps, sut);

            static IEnumerable<Tuple<int, string>> maps;
        }

        [Subject(typeof(SimpleHashTable<int, string>))]
        public class when_hashing_a_hundred_thousand_values : concern
        {
            Establish c = () =>
            {
                var rand = new Random();
                maps = Enumerable.Range(0, 100000).Select(x => new Tuple<int, string>(rand.Next(int.MaxValue), rand.Next(int.MaxValue).ToString())).ToList();
            };

            Because of = () => act(maps, sut);

            It print = () => time_x_lookups(100000, maps, sut);

            static IEnumerable<Tuple<int, string>> maps;
        }

        [Subject(typeof(SimpleHashTable<int, string>))]
        public class when_hashing_a_million_values : concern
        {
            Establish c = () =>
            {
                var rand = new Random();
                maps = Enumerable.Range(0, 1000000).Select(x => new Tuple<int, string>(rand.Next(int.MaxValue), rand.Next(int.MaxValue).ToString())).ToList();
            };

            Because of = () => act(maps, sut);

            It print = () => time_x_lookups(100000, maps, sut);

            static IEnumerable<Tuple<int, string>> maps;
        }
    }
}
