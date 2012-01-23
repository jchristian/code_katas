using System;
using System.Collections.Generic;
using UI.reflection;

namespace UI
{
    public class ArithmeticParser
    {
        readonly IDictionary<string, Func<object>> _operations;

        public ArithmeticParser()
        {
            _operations = new Dictionary<string, Func<object>>();
            _operations.Add("add", () => new Add());
            _operations.Add("subtract", () => new Subtract());
        }

        public IPerformAnOperation GetOperation(string value)
        {
            var pieces = value.Split(' ');

            if (pieces.Length != 2)
                return new DefaultOperation();

            var operation = pieces[0].ToLowerInvariant();
            if (!_operations.ContainsKey(operation))
                return new DefaultOperation();

            int converted_value;
            var could_parse = int.TryParse(pieces[1], out converted_value);
            if (!could_parse)
                return new DefaultOperation();

            //return new DynamicOperation(_operations[operation](), converted_value);
            return new ReflectionOperation(_operations[operation](), converted_value);
        }
    }
}