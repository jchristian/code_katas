using System.Collections.Generic;
using System.Linq;

namespace source.iterative
{
    public class AnotherIterativeBinaryChop : IFindAnItem
    {
        public int find(int itemToFind, IList<int> collection)
        {
            var minimum_index = 0;
            var maximum_index = collection.Count - 1;

            while (minimum_index <= maximum_index)
            {
                var mid_point = (int)new[] { minimum_index, maximum_index }.Average();
                switch(itemToFind.CompareTo(collection[mid_point]))
                {
                    case 0:
                        return mid_point;
                    case 1:
                        minimum_index = mid_point + 1;
                        break;
                    case -1:
                        maximum_index = mid_point - 1;
                        break;
                }
            }

            return -1;
        }
    }
}