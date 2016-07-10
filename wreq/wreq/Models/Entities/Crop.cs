using System;
using System.Collections.Generic;
using wreq.Models.Abstract;

namespace wreq.Models.Entities
{
    public class Crop : IHasAuthorAndName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FieldId { get; set; }
        public int CultureId { get; set; }
        public DateTime DateSeeded { get; set; }
        public int LengthIni { get; set; }
        public int LengthDev { get; set; }
        public int LengthMid { get; set; }
        public int LengthLate { get; set; }
        public string Description { get; set; }

        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

        public virtual Culture Culture { get; set; }
        public virtual Field Field { get; set; }

        public virtual ICollection<Irrigation> Irrigations { get; set; }
        public virtual ICollection<WaterLimit> WaterLimits { get; set; }




        public Crop() { }
    }
}