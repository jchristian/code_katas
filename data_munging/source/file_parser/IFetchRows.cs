using System.Collections.Generic;

namespace source.file_parser
{
    public interface IFetchRows
    {
        IEnumerable<IProvideARowOfInformation> GetAll();
    }
}