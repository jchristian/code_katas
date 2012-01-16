using System;

namespace UI.Core
{
    public interface IBuildAClass<T>
    {
        ISubstituteAMethod<T> Substituting(IWrapAnExpression<Action<T>> the_method_to_decorate);
    }
}