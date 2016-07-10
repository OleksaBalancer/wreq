using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace wreq.Models.Entities
{
    public class WeatherRecord
    {
        public WeatherRecord()
        {
        }
        public int WeatherRecordId { get; set; }
        public DateTime Date { get; set; }
        public double TempMax { get; set; }
        public double TempMin { get; set; }
        public double Humidity { get; set; }
        public double Precipitation { get; set; }
        public double WindSpeed { get; set; }
        public double AtmosphericPressure { get; set; }
        public double DaylightHours { get; set; }
        public bool IsForecasted { get; set; }
        public int FieldId { get; set; }


        [NotMapped]
        public double TempMean { get { return (TempMax + TempMin) / 2; } }

        public virtual Field Field { get; set; }
        
    }
}