using System;
using System.Collections.Generic;

namespace source.class_base_recursion
{
    public class LazyBinaryChop : IFindAnItem
    {
        Func<IFindAnItem> factory;

        public LazyBinaryChop(Func<IFindAnItem> factory)
        {
            this.factory = factory;
        }

        public int find(int itemToFind, IList<int> collection)
        {
            return factory().find(itemToFind, collection);
        }
    }
}