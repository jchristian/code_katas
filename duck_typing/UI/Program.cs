
using System;
using System.Collections.Generic;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new ArithmeticParser();
            var operations = new List<IPerformAnOperation>();

            var value = "";
            while (!value.ToLowerInvariant().Equals("compute"))
            {
                value = Console.ReadLine();
                operations.Add(parser.GetOperation(value));
            }

            var result = 0;
            foreach (var operation in operations)
            {
                result = operation.Operate(result);
            }

            Console.WriteLine(string.Format("The result is {0}", result));
            Console.ReadLine();
        }
    }
}
