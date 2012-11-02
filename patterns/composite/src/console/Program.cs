using System;
using System.Linq;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            IResult result = new EmptyResult();
            var input = Console.ReadLine();
            while (input != "Q")
            {
                var inputs = input.Split(' ');
                switch (inputs.First())
                {
                    case "add":
                        result = new Add(result, new ParsedResult(inputs.ElementAt(1)));
                        break;
                    case "subtract":
                        result = new Subtract(result, new ParsedResult(inputs.ElementAt(1)));
                        break;
                    case "multiply":
                        result = new Multiply(result, new ParsedResult(inputs.ElementAt(1)));
                        break;
                    case "divide":
                        result = new Divide(result, new ParsedResult(inputs.ElementAt(1)));
                        break;
                }

                Console.WriteLine(result.result());
                input = Console.ReadLine();
            }
        }
    }
}