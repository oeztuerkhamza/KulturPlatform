namespace KulturPlatform.API.Middleware;

/// <summary>
/// Middleware that adds a correlation ID to each request for tracing across services
/// </summary>
public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private const string CorrelationIdHeader = "X-Correlation-ID";

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check if correlation ID exists in request header
        var correlationId = context.Request.Headers[CorrelationIdHeader].FirstOrDefault();

        // If not provided, generate a new one
        if (string.IsNullOrEmpty(correlationId))
        {
            correlationId = Guid.NewGuid().ToString();
        }

        // Add to response headers
        context.Response.Headers[CorrelationIdHeader] = correlationId;

        // Store in HttpContext for access throughout the request pipeline
        context.Items["CorrelationId"] = correlationId;

        // Add to log context (when using structured logging)
        using (context.RequestServices.GetRequiredService<ILoggerFactory>()
            .CreateLogger<CorrelationIdMiddleware>()
            .BeginScope(new Dictionary<string, object> { ["CorrelationId"] = correlationId }))
        {
            await _next(context);
        }
    }
}

/// <summary>
/// Extension methods for registering correlation ID middleware
/// </summary>
public static class CorrelationIdMiddlewareExtensions
{
    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CorrelationIdMiddleware>();
    }
}
