using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace wreq.Models.ViewModels
{
    public class FieldViewModel : IValidatableObject, IHasId
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resource))]
        [Required]
        public string Name { get; set; }

        [Display(Name = "CombCulture", ResourceType = typeof(Resource))]
        //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "Required")]
        public double? CombCultureHeight { get; set; }

        [Display(Name = "Area", ResourceType = typeof(Resource))]
        [Required]
        public double Area { get; set; }

        [Display(Name = "Latitude", ResourceType = typeof(Resource))]
        [Required]
        public double Latitude { get; set; }

        [Display(Name = "Longitude", ResourceType = typeof(Resource))]
        [Required]
        public double Longitude { get; set; }

        [Display(Name = "Altitude", ResourceType = typeof(Resource))]
        [Required]
        public double Altitude { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resource))]
        [MaxLength(256)]
        public string Description { get; set; }

        [Display(Name = "SoilType", ResourceType = typeof(Resource))]
        public string SoilTypeName { get; set; }

        [Display(Name = "IrrigationType", ResourceType = typeof(Resource))]
        public string IrrigationTypeName { get; set; }

        public int SoilTypeId { get; set; }
        public int IrrigationTypeId { get; set; }

        public string AuthorId { get; set; }


        public IEnumerable<SoilTypeListViewModel> SoilTypes { get; set; }

        public IEnumerable<IrrigationTypeListViewModel> IrrigationTypes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!(CombCultureHeight > 0 || CombCultureHeight == null))
                yield return new ValidationResult(Resource.PositiveValidationError, new[] { "CombCultureHeight" });

            if (!(Area > 0))
                yield return new ValidationResult(Resource.PositiveValidationError, new[] { "Area" });

            if (!(Latitude >= -90 && Latitude <= 90))
                yield return new ValidationResult(Resource.LattitudeValidationError, new[] { "Latitude" });

            if (!(Longitude >= -180 && Longitude <= 180))
                yield return new ValidationResult(Resource.LongitudeValidationError, new[] { "Longitude" });


        }
    }
}