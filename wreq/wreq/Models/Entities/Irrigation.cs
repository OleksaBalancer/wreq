using System;

namespace wreq.Models.Entities
{
    public class Irrigation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Depth { get; set; }

        public int CropId { get; set; }

        public virtual Crop Crop { get; set; }

        public Irrigation() { }
    }
}