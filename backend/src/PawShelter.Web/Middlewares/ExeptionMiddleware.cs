using PawShelter.Core.Models;
using PawShelter.SharedKernel;

namespace PawShelter.Web.Middlewares;

public class ExeptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExeptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var error = Errors.General.InternalServer(ex.Message);
            var envelope = Envelope.Error(error);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(envelope);
        }
    }
}

public static class ExeptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExeptionMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExeptionMiddleware>();
    }
}