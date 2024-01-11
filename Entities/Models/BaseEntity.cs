namespace Entities.Models;
public class BaseEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid();
        CreateOn = DateTimeOffset.UtcNow;
        ModifyOn = DateTimeOffset.UtcNow;
    }
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? MetaDescription { get; set; }
    public DateTimeOffset CreateOn { get; set; }
    public DateTimeOffset ModifyOn { get; set; }
}
