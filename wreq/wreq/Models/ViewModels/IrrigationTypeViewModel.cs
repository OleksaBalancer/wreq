using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using wreq.Models.Entities;

namespace wreq.Models.ViewModels
{
    public class IrrigationTypeViewModel : IValidatableObject, IHasId
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resource))]
        [Required]
        public string Name { get; set; }

        [Display(Name = "WettedSoilAreaFraction", ResourceType = typeof(Resource))]
        [Required]
        public double WettedSoilAreaFraction { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resource))]
        [MaxLength(256)]
        public string Description { get; set; }

        public virtual ICollection<Field> Fields { get; set; }
        public string AuthorId { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (!(WettedSoilAreaFraction > 0 && WettedSoilAreaFraction <= 1))
                yield return new ValidationResult(Resource.Positive01ValidationError, new[] { "WettedSoilAreaFraction" });

        }
    }
}