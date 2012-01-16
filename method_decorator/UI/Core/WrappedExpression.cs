using System.Linq.Expressions;
using System.Reflection;

namespace UI.Core
{
    public class WrappedExpression<T> : IWrapAnExpression<T>
    {
        Expression<T> the_expression;

        public MethodInfo Method { get { return ((MethodCallExpression)the_expression.Body).Method; } }

        public WrappedExpression(Expression<T> theExpression)
        {
            the_expression = theExpression;
        }

        public T Compile()
        {
            return the_expression.Compile();
        }
    }
}