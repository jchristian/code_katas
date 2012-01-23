
namespace UI
{
    public class DefaultOperation : IPerformAnOperation
    {
        public int Operate(int previous_result)
        {
            return previous_result;
        }
    }
}