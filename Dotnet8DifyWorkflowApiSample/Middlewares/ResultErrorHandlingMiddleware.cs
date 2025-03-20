using System.Text.Json;
using Dotnet8DifyWorkflowApiSample.Common;

namespace Dotnet8DifyWorkflowApiSample.Middlewares;

public class ResultErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ResultErrorHandlingMiddleware> _logger;

    public ResultErrorHandlingMiddleware(RequestDelegate next, ILogger<ResultErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred during request processing.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Create a failure result (using object as a generic fallback)
        var result = Result<object>.Failure($"Internal Server Error: {exception.Message}");

        // Set the response status code to 500
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        // Serialize the result to JSON
        var jsonResult = JsonSerializer.Serialize(new
        {
            IsSuccess = result.IsSuccess,
            Value = result.Value,
            ErrorMessage = "An unhandled exception occurred during request processing."
        });

        // Write the response
        return context.Response.WriteAsync(jsonResult);
    }
}