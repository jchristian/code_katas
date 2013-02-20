using System;
using System.Collections.Generic;
using System.Linq;

namespace console
{
    public class BinarySearcher
    {
        public virtual int Find(IEnumerable<int> list, int key)
        {

            return Find(list, key, 0);
        }

        public virtual int Find(IEnumerable<int> list, int key, int index_of_first_element)
        {
            if (list == null || !list.Any())
                return -1;

            var middle_index = list.Count() / 2;
            var middle_element = list.ElementAt(middle_index);

            if (middle_element == key)
            {
                if (middle_index != 0 && list.ElementAt(middle_index - 1) == key)
                    return FindLowerHalf(list, key, index_of_first_element, middle_index);
                return middle_index + index_of_first_element;
            }

            if (key < middle_element)
                return FindLowerHalf(list, key, index_of_first_element, middle_index);

            if (key > middle_element)
                return FindUpperHalf(list, key, index_of_first_element, middle_index);

            throw new Exception("It is impossible to get here");
        }

        int FindLowerHalf(IEnumerable<int> list, int key, int index_of_first_element, int middle_index)
        {
            return Find(list.Take(middle_index), key, index_of_first_element);
        }

        int FindUpperHalf(IEnumerable<int> list, int key, int index_of_first_element, int middle_index)
        {
            return Find(list.Skip(middle_index + 1), key, index_of_first_element + (middle_index + 1));
        }
    }
}