using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Machine.Specifications.Utility;

namespace source.weather
{
    public class WeatherInformationRepository : IGetWeatherInformation
    {
        public IEnumerable<IProvideDailyWeatherInformation> GetAllTheWeatherData()
        {
            using (var reader = new FileInfo(@"data\weather.dat").OpenText())
            {
                string line;

                Enumerable.Range(1, 8).Each(x => reader.ReadLine());
                while(IsValidLine((line = reader.ReadLine())))
                {
                    yield return ParseLine(line);
                }
            }
        }

        bool IsValidLine(string line)
        {
            return line.Split(' ').Where(x => x != "").Count() > 10;
        }

        IProvideDailyWeatherInformation ParseLine(string line)
        {
            var values = line.Split(' ').Where(x => x != "").ToArray();

            return new DailyWeatherInformation(Convert.ToInt32(values[0]), Convert.ToDecimal(values[1].Replace("*", "")), Convert.ToDecimal(values[2].Replace("*", "")));
        }
    }
}