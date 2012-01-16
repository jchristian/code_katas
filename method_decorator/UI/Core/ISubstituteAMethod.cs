using System;

namespace UI.Core
{
    public interface ISubstituteAMethod<T>
    {
        T With(Action<T> the_substitution);
    }
}