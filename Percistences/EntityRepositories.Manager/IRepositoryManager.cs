using Percistences.EntityRepositories.Abstracts;

namespace Percistences.EntityRepositories.Manager;

public interface IRepositoryManager
{
    public IBlogRepository Blog { get; set; }
    public ISourceBlogRepository SourceBlog { get; set; }
    public IBlogCategoryRepository BlogCategory { get; set; }
    public IBlogCategoryDataRepository BlogCategoryData { get; set; }
    public ITagRepository Tag { get; set; }
    public IBlogTagDataRepository BlogTagData { get; set; }
    public ICategoryTagDataRepository CategoryTagData { get; set; }
}
