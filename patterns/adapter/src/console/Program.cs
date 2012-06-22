
using System;
using System.Linq;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            
            Console.WriteLine("Do you want to:");
            Console.WriteLine("1) Add");
            Console.WriteLine("2) Subtract");
            Console.WriteLine("Q) Quit");

            while((input = Console.ReadLine().ToLower()) != "q")
            {
                switch(input)
                {
                    case "1":
                        Console.WriteLine("Enter the values you want to add...");
                        var add_arguments = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => Decimal.Parse(x)).ToArray();
                        Console.WriteLine();
                        Console.WriteLine("The result is: {0}", new Calculator().Operate(new Add(), add_arguments));
                        break;
                    case "2":
                        Console.WriteLine("Enter the values you want to subtract...");
                        var subtract_arguments = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => Decimal.Parse(x)).ToArray();
                        Console.WriteLine();
                        Console.WriteLine("The result is: {0}", new Calculator().Operate(new ReversedOrderToInOrder(new ReversedSubtract()), subtract_arguments));
                        break;
                    case "q":
                        break;
                    default:
                        Console.WriteLine("Huh? I didn't catch that...");
                        break;
                }

                Console.WriteLine("Do you want to:");
                Console.WriteLine("1) Add");
                Console.WriteLine("2) Subtract");
                Console.WriteLine("Q) Quit");
            }
        }
    }

    public class Calculator
    {
        public decimal Operate(IOperateInOrder operation, params decimal[] arguments)
        {
            return operation.evaluate(arguments);
        }
    }

    public interface IOperateInOrder
    {
        decimal evaluate(decimal[] arguments);
    }

    public class Add : IOperateInOrder
    {
        public decimal evaluate(decimal[] arguments)
        {
            return arguments.Sum();
        }
    }

    public interface IOperateInReverseOrder
    {
        decimal evaluate(decimal[] arguments);
    }

    public class ReversedSubtract : IOperateInReverseOrder
    {
        public decimal evaluate(decimal[] arguments)
        {
            if (arguments.Length == 0)
                return 0;
            if (arguments.Length == 1)
                return arguments.First();

            var reversed_arguments = arguments.Reverse();

            return reversed_arguments.Skip(1).Aggregate(reversed_arguments.First(), (x, y) => x - y);
        }
    }

    public class ReversedOrderToInOrder : IOperateInOrder
    {
        readonly IOperateInReverseOrder operation;

        public ReversedOrderToInOrder(IOperateInReverseOrder operation)
        {
            this.operation = operation;
        }

        public decimal evaluate(decimal[] arguments)
        {
            return operation.evaluate(arguments.Reverse().ToArray());
        }
    }
}
