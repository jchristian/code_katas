using System;

namespace UI.reflection
{
    public class ReflectionOperation : IPerformAnOperation
    {
        object _operator;
        int value;

        public ReflectionOperation(object @operator, int value)
        {
            _operator = @operator;
            this.value = value;
        }

        public int Operate(int previous_result)
        {
            return _operator.DuckType<int>(new Func<int, int, int>(new Add().Operate), previous_result, value);
        }
    }
}