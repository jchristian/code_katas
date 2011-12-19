using System.Collections.Generic;

namespace source
{
    public interface IGetWeatherInformation
    {
        IEnumerable<IProvideDailyWeatherInformation> GetAllTheWeatherData();
    }
}