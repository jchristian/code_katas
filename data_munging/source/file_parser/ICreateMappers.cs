namespace source.file_parser
{
    public interface ICreateMappers
    {
        IBuildMaps<T> CreateAMapperFor<T>();
    }
}