using System.Collections.Generic;

namespace source.class_base_recursion
{
    public class LazyDecoratorBinaryChop : IFindAnItem
    {
        public int find(int itemToFind, IList<int> collection)
        {
            return new LazyBinaryChop(() => new DecoratorBinaryChop(new LazyDecoratorBinaryChop())).find(itemToFind, collection);
        }
    }
}