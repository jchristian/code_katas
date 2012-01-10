using System;

namespace UI.Core
{
    public interface IBuildClasses
    {
        IBuildClasses<T> Build<T>(T instance);
    }

    public interface IBuildClasses<T>
    {
        ISubstituteAMethod<T> Substituting(IWrapAnExpression<Action<T>> methodToDecorate);
    }
}