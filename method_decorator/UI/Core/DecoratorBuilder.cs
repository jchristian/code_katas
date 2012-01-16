using System;

namespace UI.Core
{
    public class DecoratorBuilder<T> : IBuildADecorator<T>
    {
        T the_object_to_decorate;
        IWrapAnExpression<Action<T>> the_method_to_decorate;
        ICreateAClass class_builder;
        ICreateDelegates delegate_factory;

        public DecoratorBuilder(T theObjectToDecorate, IWrapAnExpression<Action<T>> theMethodToDecorate, ICreateAClass classBuilder, ICreateDelegates delegateFactory)
        {
            the_object_to_decorate = theObjectToDecorate;
            the_method_to_decorate = theMethodToDecorate;
            delegate_factory = delegateFactory;
            class_builder = classBuilder;
        }

        public T With(Action<T> decoration)
        {
            return class_builder.Create(the_object_to_decorate)
                .Substituting(the_method_to_decorate)
                .With(delegate_factory.Combine(decoration, the_method_to_decorate.Compile()));
        }
    }
}