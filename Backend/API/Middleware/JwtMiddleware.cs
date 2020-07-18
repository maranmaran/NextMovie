using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace API.Middleware
{
    /// <summary>
    /// Gets JWT from cookie in request and sets auth header on the fly if it doesn't have it
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Cookies[JwtBearerDefaults.AuthenticationScheme];

            if (token != null && !context.Request.Headers.ContainsKey("Authorization"))
                context.Request.Headers.Append("Authorization", $"Bearer {token}");

            await _next.Invoke(context);
        }
    }
}
