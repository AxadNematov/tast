using System.Net;
using GeneralDomain.Extensions;

namespace GeneralDomain.Responses;

public class ApiResponseError
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }

    public ApiResponseError()
    {
    }

    public ApiResponseError(HttpStatusCode statusCode, string errorMessage)
    {
        if (HttpContextHelper.Current?.Response != null)
            HttpContextHelper.Current.Response.StatusCode = (int)statusCode;

        ErrorCode = (int)statusCode;
        ErrorMessage = errorMessage;
    }

    public ApiResponseError(int errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
    
}