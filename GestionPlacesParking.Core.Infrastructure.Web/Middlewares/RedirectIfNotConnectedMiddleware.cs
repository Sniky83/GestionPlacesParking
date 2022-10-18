using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Infrastructure.Web.Middlewares
{
    public class RedirectIfNotConnectedMiddleware
    {
        private readonly RequestDelegate _next;
        public RedirectIfNotConnectedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var id = context.Session.GetString("UserId");
            var isLoginPage = context.Request.Path.Value?.ToLower().Contains("login");

            if (string.IsNullOrEmpty(id) && (!isLoginPage.HasValue || !isLoginPage.Value))
            {
                context.Response.Redirect("/Login");
                return;
            }

            await _next.Invoke(context);
        }
    }

    public static class AuthenticationMiddlewares
    {
        public static IApplicationBuilder UseRedirectIfNotConnected(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RedirectIfNotConnectedMiddleware>();
        }
    }
}
