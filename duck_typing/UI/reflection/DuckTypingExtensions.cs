using System;
using System.Linq;
using System.Reflection;

namespace UI.reflection
{
    public static class DuckTypingExtensions
    {
        public static ResultType DuckType<ResultType>(this object instance, Delegate @delegate, params object[] parameters)
        {
            var method = instance.GetType().GetMethods().Single(x => x.Matches(@delegate.Method));

            return (ResultType)method.Invoke(instance, parameters);
        }

        public static bool Matches(this MethodInfo methodInfo, MethodInfo otherMethodInfo)
        {
            return methodInfo.Name == otherMethodInfo.Name &&
                methodInfo.OnlyContainsParameters(otherMethodInfo) &&
                methodInfo.ReturnParameter.Matches(otherMethodInfo.ReturnParameter);
        }

        public static bool OnlyContainsParameters(this MethodInfo methodInfo, MethodInfo otherMethodInfo)
        {
            return methodInfo.GetParameters().Count() == otherMethodInfo.GetParameters().Count() &&
                methodInfo.GetParameters().All(x => otherMethodInfo.GetParameters().Any(y => y.Matches(x)));
        }

        public static bool Matches(this ParameterInfo parameterInfo, ParameterInfo otherParameterInfo)
        {
            return parameterInfo.ParameterType == otherParameterInfo.ParameterType && parameterInfo.Name == otherParameterInfo.Name;
        }
    }
}