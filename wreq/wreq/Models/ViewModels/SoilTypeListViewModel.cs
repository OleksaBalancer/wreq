using System.ComponentModel.DataAnnotations;

namespace wreq.Models.ViewModels
{
    public class SoilTypeListViewModel: IHasId
    {
        public int Id { get; set; }
        [Display(Name = "NameDisplay", ResourceType = typeof(Resource))]
        [Required]
        public string Name { get; set; }
        public string AuthorId { get; set; }

    }
}