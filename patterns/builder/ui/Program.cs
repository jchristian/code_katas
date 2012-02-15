using System;
using System.Collections.Generic;

namespace ui
{
    class Program
    {
        static void Main(string[] args)
        {
            // Quadratic
            var quadratic_evaluator = new PrefixEvaluatorBuilder();
            var quadratic = new Quadratic(quadratic_evaluator);
            quadratic.Construct();

            for (int i = 1; i <= 10; i++)
                Console.WriteLine("{0} squared is {1}", i, quadratic_evaluator.Evaluate(new Dictionary<string, decimal> { { "x", i } }));

            // 1/x
            var hyperbolic_evaluator = new PrefixEvaluatorBuilder();
            var hyperbolic = new Hyperbola(hyperbolic_evaluator);
            hyperbolic.Construct();

            for (int i = 1; i <= 10; i++)
                Console.WriteLine("1 / {0} is {1}", i, hyperbolic_evaluator.Evaluate(new Dictionary<string, decimal> { { "x", i } }));

            // (1 -x)/x
            var another_hyperbolic_evaluator = new PrefixEvaluatorBuilder();
            var another_hyperbolic = new AnotherHyperbola(another_hyperbolic_evaluator);
            another_hyperbolic.Construct();

            for (int i = 1; i <= 10; i++)
                Console.WriteLine("(1 - {0}) / {0} is {1}", i, another_hyperbolic_evaluator.Evaluate(new Dictionary<string, decimal> { { "x", i } }));

            Console.ReadLine();

            //Could add a graphing builder that returns a Visual...
        }
    }
}
