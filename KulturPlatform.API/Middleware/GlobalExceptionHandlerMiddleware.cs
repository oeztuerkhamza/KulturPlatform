using FluentValidation;
using KulturPlatform.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace KulturPlatform.API.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionHandlerMiddleware> logger)
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
        catch (Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, title, errors) = exception switch
        {
            ValidationException validationException => (
                HttpStatusCode.BadRequest,
                "Validation Error",
                validationException.Errors.Select(e => new ErrorDetail
                {
                    Field = e.PropertyName,
                    Message = e.ErrorMessage
                }).ToList()
            ),
            KeyNotFoundException => (
                HttpStatusCode.NotFound,
                "Resource Not Found",
                new List<ErrorDetail> { new() { Message = exception.Message } }
            ),
            UnauthorizedAccessException => (
                HttpStatusCode.Unauthorized,
                "Unauthorized",
                new List<ErrorDetail> { new() { Message = exception.Message } }
            ),
            DomainException => (
                HttpStatusCode.BadRequest,
                "Domain Error",
                new List<ErrorDetail> { new() { Message = exception.Message } }
            ),
            _ => (
                HttpStatusCode.InternalServerError,
                "Internal Server Error",
                new List<ErrorDetail> { new() { Message = "An unexpected error occurred. Please try again later." } }
            )
        };

        context.Response.StatusCode = (int)statusCode;

        var response = new ErrorResponse
        {
            StatusCode = (int)statusCode,
            Title = title,
            Errors = errors,
            TraceId = context.TraceIdentifier
        };

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }
}

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<ErrorDetail> Errors { get; set; } = new();
    public string TraceId { get; set; } = string.Empty;
}

public class ErrorDetail
{
    public string? Field { get; set; }
    public string Message { get; set; } = string.Empty;
}
