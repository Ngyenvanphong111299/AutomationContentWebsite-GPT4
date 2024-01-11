namespace Services;

public class BaseResult<T>
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public BaseResult<T> WithStatus(bool status)
    {
        IsSuccess = status;
        return this;
    }

    public BaseResult<T> WithMessage(string message)
    {
        Message = message;
        return this;
    }

    public BaseResult<T> WithData(T data)
    {
        Data = data;
        return this;
    }
}
