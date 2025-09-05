using Domain.Exceptions;

namespace Web.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            _logger.LogError(ex, "Unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message) = exception switch
        {
            ArgumentException => (400, exception.Message),
            BusinessException => (400, exception.Message),
            CustomerNotFoundException => (404, exception.Message),
            InvalidDateRangeException => (400, exception.Message),
            NoInvoicesFoundException => (404, exception.Message),
            _ => (500, "Bir sistem hatası oluştu. Lütfen daha sonra tekrar deneyiniz.")
        };

        context.Response.StatusCode = statusCode;
        
        // MVC için error sayfasına yönlendir
        if (!context.Response.HasStarted)
        {
            context.Response.Redirect($"/Customer/Error?message={Uri.EscapeDataString(message)}");
        }
        
        await Task.CompletedTask;
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}