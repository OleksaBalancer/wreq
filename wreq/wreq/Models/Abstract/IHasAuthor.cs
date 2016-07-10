using wreq.Models.Entities;

namespace wreq.Models.Abstract
{
    public interface IHasAuthorAndName
    {
        string AuthorId { get; set; }
        string Name { get; set; }
        ApplicationUser Author { get; set; }
    }
}
