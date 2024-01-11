using Entities.Models;
using Percistences.EntityRepositories.Abstracts;

namespace Percistences.EntityRepositories.Implements;

public class BlogCategoryRepository : BaseRepository<BlogCategory>, IBlogCategoryRepository
{
    public BlogCategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}
