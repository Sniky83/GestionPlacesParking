using KeycloakCore.Keycloak;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs
{
    public class CallbackModel : PageModel
    {
        [BindProperty]
        public UserInfo? UserInfo { get; set; }

        public IActionResult OnGet()
        {
            var keycloakManager = new WebManager();
            var userInfo = keycloakManager.Callback(Request);
            if (userInfo != null)
            {
                UserInfo = userInfo;
                return Page();
            }
            else
            {
                return RedirectToAction("CallbackError");
            }
        }
    }
}
