using System;
using source;
using StructureMap;

namespace ui.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var query = ObjectFactory.GetInstance<GetTheSmallestTempuratureSpread>();

            Console.WriteLine(string.Format("The smallest tempurature spread was for June {0}", query.Fetch()));
        }
    }
}