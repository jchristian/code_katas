using System;
using System.Collections.Generic;

namespace source.file_parser
{
    public interface IProvideFileInformation : IDisposable
    {
        IContainHeaderInformation GetHeaderContaining(IEnumerable<string> get_properties);
    }

    public interface IContainHeaderInformation
    {
    }
}