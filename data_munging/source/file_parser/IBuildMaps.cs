using System.Collections.Generic;

namespace source.file_parser
{
    public interface IBuildMaps<OutType>
    {
        Map<IProvideARowOfInformation, OutType> Using(IProvideFileInformation reader);
    }

    public class MapBuilder<OutType> : IBuildMaps<OutType>
    {
        IFetchProperties _propertyProvider;

        public MapBuilder(IFetchProperties property_provider)
        {
            _propertyProvider = property_provider;
        }

        public Map<IProvideARowOfInformation, OutType> Using(IProvideFileInformation reader)
        {
            var header = reader.GetHeaderContaining(_propertyProvider.GetProperties(typeof(OutType)));

            var propertyMappers = new List<Map>();

            foreach(var property in _propertyProvider.GetProperties(typeof(OutType))
            {
                propertyMappers.Add(_rowMapFactory.Create())
            }
        }
    }
}