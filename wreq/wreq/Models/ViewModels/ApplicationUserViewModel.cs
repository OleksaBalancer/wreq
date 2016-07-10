using System.ComponentModel.DataAnnotations;

namespace wreq.Models.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "UserName", ResourceType = typeof(Resource))]

        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }

    }
}