namespace Core.Application.Responses.Abstract;

public interface ICustomResponseDto<T>
{
    public T Data { get; set; }
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }
}