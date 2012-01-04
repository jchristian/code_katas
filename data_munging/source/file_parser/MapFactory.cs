using StructureMap;

namespace source.file_parser
{
    public class MapFactory : ICreateMappers
    {
        IContainer _container;

        public MapFactory(IContainer container)
        {
            _container = container;
        }

        public IBuildMaps<OutType> CreateAMapperFor<OutType>()
        {
            return _container.GetInstance<IBuildMaps<OutType>>();
        }
    }
}