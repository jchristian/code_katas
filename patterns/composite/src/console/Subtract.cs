namespace console
{
    public class Subtract : IResult
    {
        IResult first_result;
        IResult second_result;

        public Subtract(IResult first_result, IResult second_result)
        {
            this.first_result = first_result;
            this.second_result = second_result;
        }

        public decimal result()
        {
            return first_result.result() - second_result.result();
        }
    }
}