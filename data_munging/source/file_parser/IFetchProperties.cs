using System;
using System.Collections.Generic;

namespace source.file_parser
{
    public interface IFetchProperties
    {
        IEnumerable<string> GetProperties(Type type);
    }
}