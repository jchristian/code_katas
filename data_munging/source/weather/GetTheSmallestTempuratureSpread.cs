using MoreLinq;

namespace source.weather
{
    public class GetTheSmallestTempuratureSpread : IFetchInformation<int>
    {
        IGetWeatherInformation _weatherRepository;

        public GetTheSmallestTempuratureSpread(IGetWeatherInformation weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public int Fetch()
        {
            return _weatherRepository.GetAllTheWeatherData().MinBy(x => x.GetTheTempuratureSpread()).Day;
        }
    }
}