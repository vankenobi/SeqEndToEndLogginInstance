using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ProductAPI.Middleware
{
    public class LogHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public LogHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
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
            return _next(httpContext);
        }
    }

    public static class LogHeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewareClassTemplate(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogHeaderMiddleware>();
        }
    }
}

