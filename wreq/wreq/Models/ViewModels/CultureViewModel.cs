using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wreq.Models.ViewModels
{
    public class CultureViewModel : IValidatableObject, IHasId
    {
        public int Id { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Resource))]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Ky", ResourceType = typeof(Resource))]
        [Required]
        public double YieldCoefficient { get; set; }

        [Display(Name = "KcIni", ResourceType = typeof(Resource))]
        [Required]
        public double KcIni { get; set; }

        [Display(Name = "KcMid", ResourceType = typeof(Resource))]
        [Required]
        public double KcMid { get; set; }

        [Display(Name = "KcEnd", ResourceType = typeof(Resource))]
        [Required]
        public double KcEnd { get; set; }

        [Display(Name = "Lini", ResourceType = typeof(Resource))]
        [Required]
        public int LengthIni { get; set; }

        [Display(Name = "Ldev", ResourceType = typeof(Resource))]
        [Required]
        public int LengthDev { get; set; }

        [Display(Name = "Lmid", ResourceType = typeof(Resource))]
        [Required]
        public int LengthMid { get; set; }

        [Display(Name = "Lend", ResourceType = typeof(Resource))]
        [Required]
        public int LengthLate { get; set; }


        [Display(Name = "MaxHeight", ResourceType = typeof(Resource))]
        [Required]
        public double MaxHeight { get; set; }

        [Display(Name = "MaxRoot", ResourceType = typeof(Resource))]
        [Required]
        public double MaxRootDepth { get; set; }

        [Display(Name = "WaterExtr", ResourceType = typeof(Resource))]
        [Required]
        public double WaterExtraction { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resource))]
        [MaxLength(256)]
        public string Description { get; set; }
        public string AuthorId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!(YieldCoefficient > 0))
                yield return new ValidationResult(Resource.PositiveValidationError, new[] { "YieldCoefficient" });
            if (!(KcIni > 0))
                yield return new ValidationResult(Resource.PositiveValidationError, new[] { "KcIni" });
            if (!(KcMid > 0))
                yield return new ValidationResult(Resource.PositiveValidationError, new[] { "KcMid" });
            if (!(KcEnd > 0))
                yield return new ValidationResult(Resource.PositiveValidationError, new[] { "KcEnd" });
            if (!(MaxHeight > 0))
                yield return new ValidationResult(Resource.PositiveValidationError, new[] { "MaxHeight" });
            if (!(MaxRootDepth > 0))
                yield return new ValidationResult(Resource.PositiveValidationError, new[] { "MaxRootDepth" });
            if (!(WaterExtraction > 0 && WaterExtraction <= 1))
                yield return new ValidationResult(Resource.Positive01ValidationError, new[] { "WaterExtraction" });
        }
    }
}