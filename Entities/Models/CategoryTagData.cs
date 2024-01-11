namespace Entities.Models;
public class CategoryTagData : BaseEntity
{
    public Guid BlogCategoryId { get; set; }
    public BlogCategory? BlogCategory { get; set; }
    public Guid TagId { get; set; }
    public Tag? Tag { get; set; }
}
