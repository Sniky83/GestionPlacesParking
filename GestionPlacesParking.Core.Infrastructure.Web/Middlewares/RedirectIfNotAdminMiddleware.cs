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
    public class RedirectIfNotAdminMiddleware
    {
        private readonly RequestDelegate _next;
        public RedirectIfNotAdminMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userId = context.Session.GetInt32("UserId");
            var isAdmin = context.Session.GetInt32("IsAdmin");
            bool isHistorique = context.Request.Path.Value.ToLower().Contains("historique");

            if (userId != null && isAdmin == null && isHistorique)
            {
                context.Response.Redirect("/Index");
                return;
            }

            await _next.Invoke(context);
        }
    }

    public static class AdminAuthenticationMiddlewares
    {
        public static IApplicationBuilder UseRedirectIfNotAdmin(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RedirectIfNotAdminMiddleware>();
        }
    }
}
