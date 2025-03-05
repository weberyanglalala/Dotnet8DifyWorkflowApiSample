namespace Dotnet8DifyWorkflowApiSample.Common;

public class ApiResponse
{
    public bool IsSuccess { get; set; }
    public ApiStatusCode Code { get; set; }
    public object Body { get; set; }
}

public enum ApiStatusCode
{
    Success = 200,
    BadRequest = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    InternalServerError = 500
}