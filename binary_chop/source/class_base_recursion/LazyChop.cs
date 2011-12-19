using System;
using System.Collections.Generic;

namespace source.class_base_recursion
{
    public class LazyChop : IFindAnItem
    {
        Func<IFindAnItem> factory;

        public LazyChop(Func<IFindAnItem> factory)
        {
            this.factory = factory;
        }

        public int find(int itemToFind, IList<int> collection)
        {
            return factory().find(itemToFind, collection);
        }
    }
}