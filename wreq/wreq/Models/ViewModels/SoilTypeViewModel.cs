using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wreq.Models.ViewModels
{
    public class SoilTypeViewModel : IValidatableObject, IHasId
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resource))]
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Display(Name = "FWC", ResourceType = typeof(Resource))]
        [Required]
        public double FieldWaterCapacity { get; set; }

        [Display(Name = "WWC", ResourceType = typeof(Resource))]
        [Required]
        public double WiltWaterCapacity { get; set; }

        [Display(Name = "REW", ResourceType = typeof(Resource))]
        [Required]
        public double ReadilyEvaporableWater { get; set; }

        [Display(Name = "TEW", ResourceType = typeof(Resource))]
        [Required]
        public double TotalEvaporableWater { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resource))]
        [MaxLength(256)]
        public string Description { get; set; }

        public string AuthorId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!(FieldWaterCapacity > 0 && FieldWaterCapacity <= 1))
                yield return new ValidationResult(Resource.Positive01ValidationError, new[] { "FieldWaterCapacity" });

            if (!(WiltWaterCapacity > 0 && WiltWaterCapacity <= 1))
                yield return new ValidationResult(Resource.Positive01ValidationError, new[] { "WiltWaterCapacity" });

            if (!(ReadilyEvaporableWater > 0))
                yield return new ValidationResult(Resource.PositiveValidationError, new[] { "ReadilyEvaporableWater" });

            if (!(TotalEvaporableWater > 0))
                yield return new ValidationResult(Resource.PositiveValidationError, new[] { "TotalEvaporableWater" });

            if (!(FieldWaterCapacity > WiltWaterCapacity))
            {
                yield return new ValidationResult(Resource.FWCValidationError, new[] { "FieldWaterCapacity" });
                yield return new ValidationResult(Resource.WWCValidationError, new[] { "WiltWaterCapacity" });
            }

            if (!(TotalEvaporableWater > ReadilyEvaporableWater))
            {
                yield return new ValidationResult(Resource.TEWValidationError, new[] { "TotalEvaporableWater" });
                yield return new ValidationResult(Resource.REWValidationError, new[] { "ReadilyEvaporatedWater" });
            }
        }
    }
}