namespace Entities.Models;
public class BlogCategoryData : BaseEntity
{
    public Guid BlogId { get; set; }
    public Blog? Blog { get; set; }
    public Guid BlogCategoryId { get; set; }
    public BlogCategory? BlogCategory { get; set; }
}
