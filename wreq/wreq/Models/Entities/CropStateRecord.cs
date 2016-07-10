using System;

namespace wreq.Models.Entities
{
    public class CropStateRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double PlannedIrrigation { get; set; }
        public double WaterDepletion { get; set; }
        public double EvaporationDepth { get; set; }
        public double YieldReduction { get; set; }



        public int CropId { get; set; }

        public virtual Crop Crop { get; set; }

        public CropStateRecord() { }

    }
}