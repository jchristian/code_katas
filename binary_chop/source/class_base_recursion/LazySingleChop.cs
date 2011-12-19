using System.Collections.Generic;

namespace source.class_base_recursion
{
    public class LazySingleChop : IFindAnItem
    {
        public int find(int itemToFind, IList<int> collection)
        {
            return new LazyChop(() => new SingleChop(new LazySingleChop())).find(itemToFind, collection);
        }
    }
}