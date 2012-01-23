namespace UI.dynamic
{
    public class DynamicOperation : IPerformAnOperation
    {
        object _operator;
        int value;

        public DynamicOperation(object @operator, int value)
        {
            _operator = @operator;
            this.value = value;
        }

        public int Operate(int previous_result)
        {
            return _operator.DuckType(x => x.Operate(previous_result, value));
        }
    }
}