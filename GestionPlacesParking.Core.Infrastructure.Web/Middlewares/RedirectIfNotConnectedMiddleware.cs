using GestionPlacesParking.Core.Global.Consts;
using GestionPlacesParking.Core.Global.EnvironmentVariables.Envs;
using GestionPlacesParking.Core.Infrastructure.Web.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text;

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
            dynamic? userId = SessionUtil.GetUserId(context);

            bool isLoginPage = context.Request.Path.Value.ToLower().Contains("login");


            if (userId == null && !isLoginPage)
            {
                context.Response.Redirect("/Login");
                return;
            }
            else if (userId != null && isLoginPage)
            {
                context.Response.Redirect("/Index");
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
