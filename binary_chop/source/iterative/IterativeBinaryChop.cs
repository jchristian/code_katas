using System.Collections.Generic;
using System.Linq;

namespace source.iterative
{
    public class IterativeBinaryChop : IFindAnItem
    {
        public int find(int itemToFind, IList<int> collection)
        {
            var index = 0;

            while (collection.Any())
            {
                switch (itemToFind.CompareTo(collection[collection.Count / 2]))
                {
                    case 0:
                        return index + collection.Count / 2;
                    case 1:
                        index += collection.Count/2 + 1;
                        collection = collection.Skip(collection.Count / 2 + 1).ToList();
                        break;
                    case -1:
                        collection = collection.Take(collection.Count / 2).ToList();
                        break;
                }
            }

            return -1;
        }
    }
}