using Entities.Models;
using Percistences.EntityRepositories.Abstracts;

namespace Percistences.EntityRepositories.Implements;
public class BlogTagDataRepository : BaseRepository<BlogTagData>, IBlogTagDataRepository
{
    public BlogTagDataRepository(ApplicationDbContext context) : base(context)
    {
    }
}
