namespace Entities.Models;
public class Blog : BaseEntity
{
    public string? Content { get; set; }
    public string? Keywords { get; set; }
    public string? Slug { get; set; }
    public DateTime PublishedDate { get; set; }
    public string? Author { get; set; }
    public string? AvatarPath { get; set; }
    public string? AvatarAlt { get; set; }
    public List<BlogCategoryData>? BlogCategoryDatas { get; set; }
    public List<BlogTagData>? BlogTagDatas { get; set; }

    private string GenerateSlug(string title)
    {
        var slug = title.ToLowerInvariant();
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\s-]", "");
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\s+", "-").Trim();
        slug = slug.Substring(0, slug.Length <= 50 ? slug.Length : 50).Trim();
        slug = slug.Trim('-');
        return slug;
    }
    public void UpdateSlug()
    {
        Slug = GenerateSlug(Title ?? string.Empty);
    }
}
