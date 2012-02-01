using System.Collections.Generic;

namespace UI
{
    public class FactoryRegistry
    {
        IDictionary<string, ICreateUIElements> _factories;

        public FactoryRegistry()
        {
            _factories = new Dictionary<string, ICreateUIElements>();
            _factories.Add("Circle", new CircleFactory());
            _factories.Add("Square", new SquareFactory());
        }

        public ICreateUIElements Find(string type)
        {
            return _factories[type];
        } 
    }
}