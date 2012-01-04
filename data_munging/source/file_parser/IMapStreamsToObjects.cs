using System.Collections.Generic;

namespace source.file_parser
{
    public interface IMapStreamsToObjects
    {
        IEnumerable<T> MapAll<T>(IProvideFileInformation reader);
    }
}