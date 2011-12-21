namespace source.weather
{
    public interface IProvideDailyWeatherInformation
    {
        int Day { get; }
        decimal GetTheTempuratureSpread();
    }
}