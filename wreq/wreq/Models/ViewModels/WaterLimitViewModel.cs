using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wreq.Models.ViewModels
{
    public class WaterLimitViewModel
    {
        public int Id { get; set; }

        public int CropId { get; set; }
        
        [Display(Name = "DateBegin", ResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateBegin { get; set; }

        [Display(Name = "DateEnd", ResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateEnd { get; set; }

        [Display(Name = "Volume", ResourceType = typeof(Resource))]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Required]
        public double Volume { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!(DateBegin < DateEnd))
                yield return new ValidationResult(Resource.DateEndValidationError, new[] { "DateEnd" });
            if (!(Volume > 0))
                yield return new ValidationResult(Resource.PositiveValidationError, new[] { "Volume" });
        }
    }
}