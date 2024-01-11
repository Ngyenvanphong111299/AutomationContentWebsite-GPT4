namespace Services.Service.CMS.Service.CMS.ManageBlog.Module;

public class BlogDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? MetaDescription { get; set; }
    public DateTimeOffset CreateOn { get; set; }
    public DateTimeOffset ModifyOn { get; set; }
    public string? Content { get; set; }
    public string? Keywords { get; set; }
    public string? Slug { get; set; }
    public DateTime PublishedDate { get; set; }
    public string? Author { get; set; }
    public string? AvatarPath { get; set; }
    public string? AvatarAlt { get; set; }
}

public class BlogDtoForList
{
    public List<BlogDto> Blogs { get; set; }
}

public class BlogDtoForCreate
{
    public string? Title { get; set; }
    public string? MetaDescription { get; set; }
    public string? Content { get; set; }
    public string? Keywords { get; set; }
    public string? Slug { get; set; }
    public string? Author { get; set; }
    public string? AvatarPath { get; set; }
    public string? Categories { get; set; }
    public string? Tags { get; set; }
}

public class BlogDtoForUpdate
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? MetaDescription { get; set; }
    public DateTimeOffset CreateOn { get; set; }
    public DateTimeOffset ModifyOn { get; set; }
    public string? Content { get; set; }
    public string? Keywords { get; set; }
    public string? Slug { get; set; }
    public DateTime PublishedDate { get; set; }
    public string? Author { get; set; }
    public string? AvatarPath { get; set; }
    public string? AvatarAlt { get; set; }
}

public record BlogDtoForSource(string Title, string content);

