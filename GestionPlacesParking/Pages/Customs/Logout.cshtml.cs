using GestionPlacesParking.Core.Global.EnvironmentVariables.Envs;
using KeycloakCore.Keycloak;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Clear();

            if (IsSsoEnv.IsSso)
            {
                var keycloakManager = new WebManager();
                string redirectUri = WebsiteUriEnv.WebsiteUri + "/login";

                return Redirect(keycloakManager.UserLogoutUri(redirectUri).ToString());
            }
            else
            {
                return RedirectToPage("./Login");
            }
        }
    }
}
