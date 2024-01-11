using Entities.Models;
using Percistences.EntityRepositories.Abstracts;

namespace Percistences.EntityRepositories.Implements;
public class SourceBlogRepository : BaseRepository<SourceBlog>, ISourceBlogRepository
{
    public SourceBlogRepository(ApplicationDbContext context) : base(context)
    {
    }
}
