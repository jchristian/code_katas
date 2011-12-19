using MoreLinq;
using source.specs;

namespace source
{
    public class GetTheSmallestTempuratureSpread : IFetchWeatherInformation<int>
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