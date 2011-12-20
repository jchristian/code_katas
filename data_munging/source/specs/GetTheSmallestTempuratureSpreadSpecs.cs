using System.Linq;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.moq;
using Machine.Specifications;

namespace source.specs
{
    public class GetTheSmallestTempuratureSpreadSpecs
    {
        public abstract class concern : Observes<IFetchWeatherInformation<int>,
                                            GetTheSmallestTempuratureSpread> {}

        [Subject(typeof(GetTheSmallestTempuratureSpreadSpecs))]
        public class when_getting_the_day_of_the_smallest_tempurature_spread : concern
        {
            static int the_returned_day;
            static int the_day_of_the_smallest_tempurature_spread;

            Establish c = () =>
            {
                the_day_of_the_smallest_tempurature_spread = 2;
                

                var the_daily_weather_with_the_smallest_tempurature_spread = fake.an<IProvideDailyWeatherInformation>();
                the_daily_weather_with_the_smallest_tempurature_spread.setup(x => x.GetTheTempuratureSpread()).Return(1);
                the_daily_weather_with_the_smallest_tempurature_spread.setup(x => x.Day).Return(the_day_of_the_smallest_tempurature_spread);

                var all_the_weather_information = Enumerable.Range(1, 100).Select(x =>
                {
                    var daily_weather = fake.an<IProvideDailyWeatherInformation>();
                    daily_weather.setup(y => y.GetTheTempuratureSpread()).Return(30);
                    daily_weather.setup(y => y.Day).Return(1);
                    return daily_weather;
                }).ToList();
                all_the_weather_information.Add(the_daily_weather_with_the_smallest_tempurature_spread);

                var weather_repository = depends.on<IGetWeatherInformation>();
                weather_repository.setup(x => x.GetAllTheWeatherData()).Return(all_the_weather_information);
            };

            Because of = () =>
                the_returned_day = sut.Fetch();

            It should_return_the_day_of_the_smallest_tempurature_spread = () =>
                the_returned_day.ShouldEqual(the_day_of_the_smallest_tempurature_spread);
        }
    }
}