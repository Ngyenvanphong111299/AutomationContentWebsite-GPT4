using Entities.Models;
using Percistences.EntityRepositories.Abstracts;

namespace Percistences.EntityRepositories.Implements;

public class CategoryTagDataRepository : BaseRepository<CategoryTagData>, ICategoryTagDataRepository
{
    public CategoryTagDataRepository(ApplicationDbContext context) : base(context)
    {
    }
}
