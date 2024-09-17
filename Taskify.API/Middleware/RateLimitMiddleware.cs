using System.Text.Json;

namespace Taskify.API.Middleware;

public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;

    public RateLimitMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
        {
            context.Response.ContentType = "application/json";
            var response = new { message = "Você fez muitas requisições. Por favor, tente novamente mais tarde." };
            var jsonResponse = JsonSerializer.Serialize(response);

            context.Response.ContentLength = jsonResponse.Length;
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
