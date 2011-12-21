using System;
using source.football;
using source.weather;

namespace ui.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var weatherQuery = new GetTheSmallestTempuratureSpread(new WeatherInformationRepository());
            var footballQuery = new GetTheTeamWithTheSmallestPointSpread(new FootballInformationRepository());

            Console.WriteLine(string.Format("The smallest tempurature spread was for June {0}", weatherQuery.Fetch()));
            Console.WriteLine(string.Format("The team with the smallest point spread was {0}", footballQuery.Fetch().Name));
            Console.ReadLine();
        }
    }
}