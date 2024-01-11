using Entities.Models;
using Percistences.EntityRepositories.Abstracts;

namespace Percistences.EntityRepositories.Implements;
public class BlogCategoryDataRepository : BaseRepository<BlogCategoryData>, IBlogCategoryDataRepository
{
    public BlogCategoryDataRepository(ApplicationDbContext context) : base(context)
    {
    }
}
