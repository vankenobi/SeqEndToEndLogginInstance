using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CustomerAPI.Middleware
{
    public class LogHeaderMiddleWare
    {
        private readonly RequestDelegate _next;

        public LogHeaderMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var header = httpContext.Request.Headers["requestId"];
            string requestId;

            if (header.Count > 0)
            {
                requestId = header[0] ?? Guid.NewGuid().ToString();
            }
            else
            {
                requestId = Guid.NewGuid().ToString();
            }

            httpContext.Items["requestId"] = requestId;
            await _next(httpContext);
        }
    }

    public static class LogHeaderMiddleWareExtensions
    {
        public static IApplicationBuilder UseMiddlewareClassTemplate(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogHeaderMiddleWare>();
        }
    }
}

