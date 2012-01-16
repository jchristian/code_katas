using System.Reflection;

namespace UI.Core
{
    public interface IWrapAnExpression<T>
    {
        MethodInfo Method { get; }

        T Compile();
    }
}