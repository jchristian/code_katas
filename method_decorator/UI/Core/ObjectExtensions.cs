using System;
using System.Linq.Expressions;
using StructureMap;

namespace UI.Core
{
    public static class ObjectExtensions
    {
        public static IBuildADecorator<T> Decorate<T>(this T the_object_to_decorate, Expression<Action<T>> the_method_to_decorate)
        {
            return ObjectFactory.With(the_object_to_decorate)
                .With(the_method_to_decorate)
                .GetInstance<DecoratorBuilder<T>>();
        }
    }
}