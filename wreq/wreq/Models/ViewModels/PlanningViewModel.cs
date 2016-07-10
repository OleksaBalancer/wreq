using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wreq.Models.ViewModels
{
    public class PlanningViewModel: IValidatableObject
    {
        public int CropId { get; set; }

        public DateTime DateSeeded { get; set; }
        public int LengthIni { get; set; }
        public int LengthDev { get; set; }
        public int LengthMid { get; set; }
        public int LengthLate { get; set; }

        [Display(Name = "YieldReduction", ResourceType = typeof(Resource))]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double? YieldReduction { get; set; }


        [Display(Name = "Crop", ResourceType = typeof(Resource))]
        public string CropName { get; set; }

        [Display(Name = "DateBegin", ResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateBegin { get; set; }

        [Display(Name = "DateEnd", ResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateEnd { get; set; }

        public IEnumerable<IrrigationViewModel> Irrigations { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!(DateBegin < DateEnd))
                yield return new ValidationResult(Resource.DateEndValidationError, new[] { "DateEnd" });
            if (!(DateBegin >= DateSeeded))
                yield return new ValidationResult(Resource.PlanningDateBeginError, new[] { "DateBegin" });
            if (!(DateEnd <= DateSeeded.AddDays(LengthIni + LengthDev + LengthMid + LengthLate)))
                yield return new ValidationResult(Resource.PlanningDateEndTooBigError, new[] { "DateEnd" });
        }
    }
}