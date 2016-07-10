using ForecastIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using wreq.BL.Abstract;
using wreq.Models.Entities;

namespace wreq.BL
{
    public class WeatherManager: IWeatherManager
    {
        public WeatherRecord GetWeatherRecord(DateTime date, double latitude, double longitude)
        {
            var request = new ForecastIORequest(WebConfigurationManager.AppSettings["WeatherAPI"], (float)latitude, (float)longitude, date, Unit.si);
            var response = request.Get();

            return new WeatherRecord()
            {
                Date = date,
                DaylightHours = (response.daily.data.First().sunsetTime - response.daily.data.First().sunriseTime) / 3600.0,
                AtmosphericPressure = response.daily.data.First().pressure,
                Humidity = response.daily.data.First().humidity,
                TempMax = response.daily.data.First().temperatureMax,
                TempMin = response.daily.data.First().temperatureMin,
                Precipitation = response.daily.data.First().precipAccumulation, 
                WindSpeed = response.daily.data.First().windSpeed
            };
        }
        
        public WeatherRecord GetAverageHistoricalWeatherRecord(DateTime date, double latitude, double longitude, int years)
        {
            List<WeatherRecord> bufWeatherRecords = new List<WeatherRecord>();

            for (int i = 1; i <= years; i++)
            {
                var request = new ForecastIORequest(WebConfigurationManager.AppSettings["WeatherAPI"], (float)latitude, (float)longitude, date.AddYears(-i), Unit.si);
                var response = request.Get();

                bufWeatherRecords.Add(new WeatherRecord()
                {
                    Date = date.AddYears(-i),
                    DaylightHours = (response.daily.data.First().sunsetTime - response.daily.data.First().sunriseTime) / 3600.0,
                    AtmosphericPressure = response.daily.data.First().pressure,
                    Humidity = response.daily.data.First().humidity,
                    TempMax = response.daily.data.First().temperatureMax,
                    TempMin = response.daily.data.First().temperatureMin,
                    Precipitation = response.daily.data.First().precipAccumulation,
                    WindSpeed = response.daily.data.First().windSpeed
                });
            }

           return new WeatherRecord()
            {
                Date = date,
                DaylightHours = bufWeatherRecords.Average(x => x.DaylightHours),
                AtmosphericPressure = bufWeatherRecords.Average(x => x.AtmosphericPressure),
                Humidity = bufWeatherRecords.Average(x => x.Humidity),
                TempMax = bufWeatherRecords.Average(x => x.TempMax),
                TempMin = bufWeatherRecords.Average(x => x.TempMin),
                Precipitation = bufWeatherRecords.Average(x => x.Precipitation),
                WindSpeed = bufWeatherRecords.Average(x => x.WindSpeed)
            };
        }
    }
}