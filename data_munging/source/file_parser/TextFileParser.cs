using System.Collections.Generic;

namespace source.file_parser
{
    public class TextFileParser : IGetInformation
    {
        ICreateFileStreams _fileStreamFactory;
        IMapStreamsToObjects _streamMapper;

        public TextFileParser(ICreateFileStreams file_stream_factory, IMapStreamsToObjects stream_mapper)
        {
            _fileStreamFactory = file_stream_factory;
            _streamMapper = stream_mapper;
        }

        public IEnumerable<T> ReadFile<T>(string fileLocation)
        {
            using (var reader = _fileStreamFactory.GetStreamFor(fileLocation))
            {
                return _streamMapper.MapAll<T>(reader);
            }
        }
    }
}