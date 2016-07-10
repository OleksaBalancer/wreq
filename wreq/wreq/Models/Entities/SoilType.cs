using System.Collections.Generic;
using wreq.Models.Abstract;

namespace wreq.Models.Entities
{
    public class SoilType: IHasAuthorAndName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double FieldWaterCapacity { get; set; }
        public double WiltWaterCapacity { get; set; }
        public double ReadilyEvaporableWater { get; set; }
        public double TotalEvaporableWater { get; set; }
        public string Description { get; set; }

        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Field> Fields { get; set; }

        public SoilType() { }
    }
}