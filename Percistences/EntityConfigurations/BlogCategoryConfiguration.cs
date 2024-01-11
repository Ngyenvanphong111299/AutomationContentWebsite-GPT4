using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Percistences.EntityConfigurations;

public class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategory>
{
    public void Configure(EntityTypeBuilder<BlogCategory> builder)
    {
        builder.HasMany(bc => bc.BlogCategoryDatas)
            .WithOne(bcd => bcd.BlogCategory)
            .HasForeignKey(bcd => bcd.BlogCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(bc => bc.CategoryTagDatas)
            .WithOne(ctd => ctd.BlogCategory)
            .HasForeignKey(ctd => ctd.BlogCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(builder => builder.SubCategories)
            .WithOne(builder => builder.ParentCategory)
            .HasForeignKey(builder => builder.ParentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
