using System;

namespace UI.Core
{
    public interface IBuildADecorator<T>
    {
        T With(Action<T> decoration);
    }
}