using System;
using UI.Specs.Core;

namespace UI.Core
{
    public class DecoratorBuilder<T> : IBuildADecorator<T>
    {
        T _instance;
        IWrapAnExpression<Action<T>> _methodToDecorate;
        IBuildClasses _classBuilder;
        ICreateDelegates _delegateFactory;

        public DecoratorBuilder(T instance, IWrapAnExpression<Action<T>> methodToDecorate, IBuildClasses classBuilder, ICreateDelegates delegateFactory)
        {
            _instance = instance;
            _methodToDecorate = methodToDecorate;
            _delegateFactory = delegateFactory;
            _classBuilder = classBuilder;
        }

        public T With(Action<T> decoration)
        {
            return _classBuilder.Build(_instance)
                .Substituting(_methodToDecorate)
                .With(_delegateFactory.Combine(decoration, _methodToDecorate.Compile()));
        }
    }
}