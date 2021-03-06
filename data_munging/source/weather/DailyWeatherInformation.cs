﻿using System;

namespace source.weather
{
    public class DailyWeatherInformation : IProvideDailyWeatherInformation
    {
        decimal _maximumTempurature;
        decimal _minimumTempurature;

        public DailyWeatherInformation(int day, decimal maximumTempurature, decimal minimumTempurature)
        {
            _maximumTempurature = maximumTempurature;
            _minimumTempurature = minimumTempurature;
            Day = day;
        }

        public int Day { get; private set; }

        public decimal GetTheTempuratureSpread()
        {
            return Math.Abs(_maximumTempurature - _minimumTempurature);
        }
    }
}