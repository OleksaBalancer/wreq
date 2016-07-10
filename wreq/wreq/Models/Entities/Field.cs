using System.Collections.Generic;
using wreq.Models.Abstract;

namespace wreq.Models.Entities
{
    public class Field : IHasAuthorAndName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? CombCultureHeight { get; set; }
        public double Area { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public string Description { get; set; }
        public int SoilTypeId { get; set; }
        public int IrrigationTypeId { get; set; }
        public virtual SoilType SoilType { get; set; }
        public virtual IrrigationType IrrigationType { get; set; }
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Crop> Crops { get; set; }
        public virtual ICollection<WeatherRecord> WeatherRecords { get; set; }


        public Field() { }

    }
}