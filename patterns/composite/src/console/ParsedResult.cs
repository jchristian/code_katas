using System;

namespace console
{
    public class ParsedResult : IResult
    {
        string first_result;

        public ParsedResult(string first_result)
        {
            this.first_result = first_result;
        }

        public decimal result()
        {
            return Decimal.Parse(first_result);
        }
    }
}