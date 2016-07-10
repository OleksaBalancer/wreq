using System.ComponentModel.DataAnnotations;

namespace wreq.Models.ViewModels
{
    public class CropListViewModel:IHasId
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resource))]
        [Required]
        public string Name { get; set; }

        public string AuthorId { get; set; }
    }
}