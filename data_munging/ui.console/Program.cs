using System;
using source;

namespace ui.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var query = new GetTheSmallestTempuratureSpread(new WeatherInformationRepository());

            Console.WriteLine(string.Format("The smallest tempurature spread was for June {0}", query.Fetch()));
            Console.ReadLine();
        }
    }
}