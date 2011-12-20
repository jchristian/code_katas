using developwithpassion.specifications.moq;
using Machine.Specifications;

namespace source.specs
{
    public class WeatherInformationRepositorySpecs
    {
        public abstract class concern : Observes<IGetWeatherInformation,
                                            WeatherInformationRepository> {}

        [Subject(typeof(WeatherInformationRepository))]
        public class when_getting_all_the_weather_information : concern
        {
            //It should_return_the_all_the_weather_information_from_the_data_source = () => 
        }
    }
}