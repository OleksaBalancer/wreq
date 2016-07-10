using System;

namespace wreq.Models.Entities
{
    public class WaterLimit
    {
        public int Id { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public double Volume { get; set; }

        public int CropId { get; set; }
        public virtual Crop Crop { get; set; }

        public WaterLimit() { }
    }
}