using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace source.specs
{
    public static class SpecExtensions
    {
        public static void ShouldEqual<T>(this T actual, T expected, string message)
        {
            try
            {
                actual.ShouldEqual(expected);
            }
            catch (SpecificationException ex)
            {
                throw new SpecificationException(string.Format(message, expected, actual), ex);
            }
        }

        public static string ToStringOfItems<T>(this IEnumerable<T> list)
        {
            return string.Format("[{0}]", list.Aggregate("", (x, y) => x + y.ToString() + ", "));
        }
    }
}