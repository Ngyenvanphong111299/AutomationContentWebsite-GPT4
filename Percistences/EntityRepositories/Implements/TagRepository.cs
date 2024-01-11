using Entities.Models;
using Percistences.EntityRepositories.Abstracts;

namespace Percistences.EntityRepositories.Implements;

public class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(ApplicationDbContext context) : base(context)
    {
    }
}
