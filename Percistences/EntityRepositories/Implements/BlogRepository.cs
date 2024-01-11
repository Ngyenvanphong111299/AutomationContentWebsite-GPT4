using Entities.Models;
using Percistences.EntityRepositories.Abstracts;

namespace Percistences.EntityRepositories.Implements;
public class BlogRepository : BaseRepository<Blog>, IBlogRepository
{
    public BlogRepository(ApplicationDbContext context) : base(context)
    {
    }
}
