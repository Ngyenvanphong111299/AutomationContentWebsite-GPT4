namespace Percistences.BaseResults;
public class EntityResult<T>
{
    public T? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }

    public EntityResult<T> WithStatus(bool status)
    {
        IsSuccess = status;
        return this;
    }

    public EntityResult<T> WithData(T data)
    {
        Data = data;
        return this;
    }
}
