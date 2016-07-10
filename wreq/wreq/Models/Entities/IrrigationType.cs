using System.Collections.Generic;
using wreq.Models.Abstract;

namespace wreq.Models.Entities
{
    public class IrrigationType : IHasAuthorAndName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double WettedSoilAreaFraction { get; set; }
        public string Description { get; set; }

        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Field> Fields { get; set; }

        public IrrigationType() { }
    }
}