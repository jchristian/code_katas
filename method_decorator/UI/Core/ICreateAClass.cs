namespace UI.Core
{
    public interface ICreateAClass
    {
        IBuildAClass<T> Create<T>(T the_object_to_build);
    }
}