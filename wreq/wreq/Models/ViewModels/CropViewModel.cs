using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wreq.Models.ViewModels
{
    public class CropViewModel : IValidatableObject, IHasId
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resource))]
        [Required]
        public string Name { get; set; }

        [Display(Name = "DateSeeded", ResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime DateSeeded { get; set; }

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

        [Display(Name = "Description", ResourceType = typeof(Resource))]
        [MaxLength(255)]
        public string Description { get; set; }

        [Display(Name = "CultureC", ResourceType = typeof(Resource))]
        public string CultureName { get; set; }

        [Display(Name = "Field", ResourceType = typeof(Resource))]
        public string FieldName { get; set; }

        public int FieldId { get; set; }

        public int CultureId { get; set; }
        public string AuthorId { get; set; }


        public IEnumerable<FieldListViewModel> Fields { get; set; }

        public IEnumerable<CultureListViewModel> Cultures { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!(LengthIni > 0))
                yield return new ValidationResult(Resource.PositiveValidationError, new[] { "LengthIni" });
            if (!(LengthDev > 0))
                yield return new ValidationResult(Resource.PositiveValidationError, new[] { "LengthDev" });
            if (!(LengthMid > 0))
                yield return new ValidationResult(Resource.PositiveValidationError, new[] { "LengthMid" });
            if (!(LengthLate > 0))
                yield return new ValidationResult(Resource.PositiveValidationError, new[] { "LengthLate" });

        }
    }
}