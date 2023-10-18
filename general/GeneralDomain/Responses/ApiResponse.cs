namespace GeneralDomain.Responses;

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; } = true;
    public T Result { get; set; }
    public ApiResponseError Error { get; set; }

    public ApiResponse()
    {
    }

    public ApiResponse(T result)
    {
        Result = result;
        IsSuccess = true;
    }

    public ApiResponse(ApiResponseError error)
    {
        Error = error;
        IsSuccess = false;
    }

    public static implicit operator ApiResponse<T>(T result)
    {
        return new ApiResponse<T>(result);
    }

    public static implicit operator ApiResponse<T>(ApiResponseError error)
    {
        return new ApiResponse<T>(error);
    }
}