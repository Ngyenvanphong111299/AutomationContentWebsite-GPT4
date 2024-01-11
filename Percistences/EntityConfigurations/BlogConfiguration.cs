using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Percistences.EntityConfigurations;
public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.HasMany(b => b.BlogCategoryDatas)
            .WithOne(bcd => bcd.Blog)
            .HasForeignKey(bcd => bcd.BlogId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.BlogTagDatas)
            .WithOne(btd => btd.Blog)
            .HasForeignKey(btd => btd.BlogId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
