using System.Collections.Generic;
using wreq.Models.Abstract;

namespace wreq.Models.Entities
{
    public class Culture : IHasAuthorAndName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double YieldCoefficient { get; set; }
        public double KcIni { get; set; }
        public double KcMid { get; set; }
        public double KcEnd { get; set; }
        public double MaxHeight { get; set; }
        public double MaxRootDepth { get; set; }
        public double WaterExtraction { get; set; }
        public int LengthIni { get; set; }
        public int LengthDev { get; set; }
        public int LengthMid { get; set; }
        public int LengthLate { get; set; }
        public string Description { get; set; }

        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Crop> Crops { get; set; }

        public Culture() { }

    }
}