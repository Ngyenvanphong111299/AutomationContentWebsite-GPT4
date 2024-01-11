using Percistences.EntityRepositories.Abstracts;
using Percistences.EntityRepositories.Implements;

namespace Percistences.EntityRepositories.Manager;
public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationDbContext _context;
    public RepositoryManager(ApplicationDbContext context)
    {
        _context = context;
        Blog = new Lazy<IBlogRepository>(() => new BlogRepository(_context)).Value;
        SourceBlog = new Lazy<ISourceBlogRepository>(() => new SourceBlogRepository(_context)).Value;
        BlogCategory = new Lazy<IBlogCategoryRepository>(() => new BlogCategoryRepository(_context)).Value;
        BlogCategoryData = new Lazy<IBlogCategoryDataRepository>(() => new BlogCategoryDataRepository(_context)).Value;
        Tag = new Lazy<ITagRepository>(() => new TagRepository(_context)).Value;
        BlogTagData = new Lazy<IBlogTagDataRepository>(() => new BlogTagDataRepository(_context)).Value;
        CategoryTagData = new Lazy<ICategoryTagDataRepository>(() => new CategoryTagDataRepository(_context)).Value;
    }
    public IBlogRepository Blog { get; set; }
    public IBlogCategoryRepository BlogCategory { get; set; }
    public IBlogCategoryDataRepository BlogCategoryData { get; set; }
    public ITagRepository Tag { get; set; }
    public IBlogTagDataRepository BlogTagData { get; set; }
    public ICategoryTagDataRepository CategoryTagData { get; set; }
    public ISourceBlogRepository SourceBlog { get; set; }
}
