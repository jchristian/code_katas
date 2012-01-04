using System.Collections.Generic;

namespace source.file_parser
{
    public interface IGetInformation
    {
        IEnumerable<T> ReadFile<T>(string fileLocation);
    }
}