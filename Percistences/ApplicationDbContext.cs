using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Percistences;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<SourceBlog> SourceBlogs { get; set; }
    public DbSet<BlogCategory> BlogCategories { get; set; }
    public DbSet<BlogCategoryData> BlogCategoryDatas { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<BlogTagData> BlogTagDatas { get; set; }
    public DbSet<CategoryTagData> CategoryTagDatas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
