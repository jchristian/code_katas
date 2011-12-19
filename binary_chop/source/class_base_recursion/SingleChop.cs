using System;
using System.Collections.Generic;
using System.Linq;

namespace source.class_base_recursion
{
    public class SingleChop : IFindAnItem
    {
        IFindAnItem binary_chopper;

        public SingleChop(IFindAnItem binary_chopper)
        {
            this.binary_chopper = binary_chopper;
        }

        public int find(int itemToFind, IList<int> collection)
        {
            if(!collection.Any())
                return -1;

            var foundIndex = 0;
            switch(itemToFind.CompareTo(collection[collection.Count / 2]))
            {
                case 0:
                    return collection.Count/2;
                case 1:
                    foundIndex = binary_chopper.find(itemToFind, collection.Skip(collection.Count/2 + 1).ToList());
                    return foundIndex != -1 ? foundIndex + collection.Count / 2 + 1 : -1;
                case -1:
                    foundIndex = binary_chopper.find(itemToFind, collection.Take(collection.Count/2).ToList());
                    return foundIndex != -1 ? foundIndex : -1;
            }

            throw new Exception();
        }
    }
}