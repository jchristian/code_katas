using System.Collections.Generic;

namespace source
{
    public interface IFindAnItem
    {
        int find(int itemToFind, IList<int> collection);
    }
}