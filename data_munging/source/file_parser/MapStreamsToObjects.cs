using System.Collections.Generic;
using System.Linq;

namespace source.file_parser
{
    public class MapStreamsToObjects : IMapStreamsToObjects
    {
        ICreateMappers _mappingInformationProvider;
        IFetchRows _rowFetcher;

        public IEnumerable<T> MapAll<T>(IProvideFileInformation reader)
        {
            var mapper = _mappingInformationProvider.CreateAMapperFor<T>().Using(reader);
            return _rowFetcher.GetAll().Select(x => mapper(x));
        }
    }
}