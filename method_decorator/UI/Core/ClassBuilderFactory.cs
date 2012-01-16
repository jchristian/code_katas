namespace UI.Core
{
    public class ClassBuilderFactory : ICreateAClass
    {
        ICreateObjects object_factory;

        public ClassBuilderFactory(ICreateObjects objectFactory)
        {
            object_factory = objectFactory;
        }

        public IBuildAClass<T> Create<T>(T the_object_to_build)
        {
            return object_factory.With(the_object_to_build).GetInstance<IBuildAClass<T>>();
        }
    }
}