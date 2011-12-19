namespace source
{
    public interface IProvideDailyWeatherInformation
    {
        int Day { get; }
        decimal GetTheTempuratureSpread();
    }
}