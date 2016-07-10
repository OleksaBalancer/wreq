using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wreq.Models.ViewModels
{
    public class IrrigationViewModel :IValidatableObject
    {
        public int Id { get; set; }

        public int CropId { get; set; }

        [Display(Name = "Date", ResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Volume", ResourceType = typeof(Resource))]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Required]
        public double Volume { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!(Volume > 0))
                yield return new ValidationResult(Resource.PositiveValidationError, new[] { "Volume" });
        }
    }
}