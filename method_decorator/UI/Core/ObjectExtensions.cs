using System;
using System.Linq.Expressions;
using StructureMap;

namespace UI.Core
{
    public static class ObjectExtensions
    {
        public static IBuildADecorator<T> Decorate<T>(this T instance, Expression<Action<T>> methodToDecorate)
        {
            return ObjectFactory.With(instance)
                .With(methodToDecorate)
                .GetInstance<DecoratorBuilder<T>>();
        }
    }
}