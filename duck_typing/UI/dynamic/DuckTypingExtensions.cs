using System;

namespace UI.dynamic
{
    public static class DuckTypingExtensions
    {
        public static ResultType DuckType<ResultType>(this object instance, Func<dynamic, ResultType> func)
        {
            return func(instance);
        }
    }
}