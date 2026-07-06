using System.Net;
using System.Text.Json;
using MarketAPI.Application.Exceptions;

namespace MarketAPI.Presentation.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    public GlobalExceptionMiddleware(RequestDelegate next,  ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(httpContext, e);
        }
    }

    private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";
        var message = "Sunucu kaynaklı hata oluştu, lutfen yoneticiye bildirin!";
        var statusCode = (int)HttpStatusCode.InternalServerError;
        switch (exception)
        {
            case NotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                message = exception.Message;
                _logger.LogWarning(message);
                break;
            default:
                _logger.LogError(exception, $"Sunucu kaynakli hata olustu: {exception.Message}");
                break;
        }
        httpContext.Response.StatusCode = statusCode;
        var response = new
        {
            StatusCode = statusCode,
            Message = message
        };
        var jsonResponse = JsonSerializer.Serialize(response);
        return httpContext.Response.WriteAsync(jsonResponse);
    }   
}