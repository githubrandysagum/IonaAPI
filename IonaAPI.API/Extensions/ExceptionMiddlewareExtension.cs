using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace IonaAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void UseIonaExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
