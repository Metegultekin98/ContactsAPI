using Core.Application.Responses.Abstract;
using Newtonsoft.Json;

namespace Core.Application.Responses.Concrete;

public class CustomResponseDto<T> : ICustomResponseDto<T>
{
    public T Data { get; set; }
    [JsonIgnore]
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }

    public static CustomResponseDto<T> Success(int statusCode, T data, bool isSuccess)
    {
        return new CustomResponseDto<T> { Data = data, StatusCode = statusCode, IsSuccess = isSuccess };
    }
    public static CustomResponseDto<T> Success(int statusCode, bool isSuccess)
    {
        return new CustomResponseDto<T> { StatusCode = statusCode, IsSuccess = isSuccess };
    }
}