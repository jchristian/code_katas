using System.Collections.Generic;

namespace source.weather
{
    public interface IGetWeatherInformation
    {
        IEnumerable<IProvideDailyWeatherInformation> GetAllTheWeatherData();
    }
}