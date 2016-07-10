using System;
using wreq.Models.Entities;

namespace wreq.BL.Abstract
{
    public interface IWeatherManager
    {
        WeatherRecord GetWeatherRecord(DateTime date, double latitude, double longitude);

        WeatherRecord GetAverageHistoricalWeatherRecord(DateTime date, double latitude, double longitude, int years);
    }
}
