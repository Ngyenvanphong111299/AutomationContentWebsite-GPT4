namespace Entities.Models
{
    public class BlogCategory : BaseEntity
    {
        public string? Content { get; set; }
        public int Level { get; set; }
        public string? VirtualPath { get; set; }
        public Guid ParentId { get; set; }
        public BlogCategory? ParentCategory { get; set; }
        public List<BlogCategory>? SubCategories { get; set; }
        public List<BlogCategoryData>? BlogCategoryDatas { get; set; }
        public List<CategoryTagData>? CategoryTagDatas { get; set; }
    }
}
