using System.Collections.Generic;
using System.Linq;

namespace source.recursive
{
    public class RecursiveBinaryChop : IFindAnItem
    {
        public int find(int itemToFind, IList<int> collection)
        {
            return find(itemToFind, collection, 0);
        }
        
        int find(int itemToFind, IList<int> collection, int currentPosition)
        {
            if (!collection.Any())
                return -1;

            switch (itemToFind.CompareTo(collection[collection.Count / 2]))
            {
                case -1:
                    return find_when_less_than(itemToFind, collection, currentPosition);
                case 1:
                    return find_when_greater_than(itemToFind, collection, currentPosition);
                default:
                    return find_when_equal_to(collection, currentPosition);
            }
        }

        int find_when_equal_to(IList<int> collection, int currentPosition)
        {
            return currentPosition + collection.Count / 2;
        }

        int find_when_greater_than(int itemToFind, IList<int> collection, int currentPosition)
        {
            return find(itemToFind, collection.Skip(collection.Count / 2 + 1).ToList(), currentPosition + collection.Count / 2 + 1);
        }

        int find_when_less_than(int itemToFind, IList<int> collection, int currentPosition)
        {
            return find(itemToFind, collection.Take(collection.Count / 2).ToList(), currentPosition);
        }
    }
}