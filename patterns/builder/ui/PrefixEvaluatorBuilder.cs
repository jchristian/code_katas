using System;
using System.Collections.Generic;
using System.Linq;

namespace ui
{
    public class PrefixEvaluatorBuilder : IBuildFunctions
    {
        Func<decimal> _function;
        Func<string, decimal> _resolve;
        Stack<Func<decimal>> _values;

        public PrefixEvaluatorBuilder()
        {
            _values = new Stack<Func<decimal>>();
        }

        public IBuildFunctions Constant(decimal value)
        {
            if (_function == null)
                _function = () => value;
            else
                _values.Push(() => value);

            return this;
        }

        public IBuildFunctions Variable(string name)
        {
            if (_function == null)
                _function = () => _resolve(name);
            else
                _values.Push(() => _resolve(name));
            
            return this;
        }

        public IBuildFunctions Add()
        {
            var previous_function = _function;
            _function = () => previous_function() + _values.Pop()();
            return this;
        }

        public IBuildFunctions Subtract()
        {
            var previous_function = _function;
            _function = () => previous_function() - _values.Pop()();
            return this;
        }

        public IBuildFunctions Multiply()
        {
            var previous_function = _function;
            _function = () => previous_function() * _values.Pop()();
            return this;
        }

        public IBuildFunctions Divide()
        {
            var previous_function = _function;
            _function = () => previous_function() / _values.Pop()();
            return this;
        }

        public decimal Evaluate(IDictionary<string, decimal> variable_values)
        {
            var temp_values = new Stack<Func<decimal>>(_values.ToList());
            _resolve = name => variable_values[name];

            var result = _function();
            _values = temp_values;
            return result;
        }
    }
}