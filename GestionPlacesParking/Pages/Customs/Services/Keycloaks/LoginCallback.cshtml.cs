using GestionPlacesParking.Core.Global.Consts;
using KeycloakCore.Keycloak;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs
{
    public class CallbackModel : PageModel
    {
        [BindProperty]
        public UserInfo UserInfo { get; set; } = null;

        public IActionResult OnGet()
        {
            var keycloakManager = new WebManager();
            var userInfo = keycloakManager.Callback(Request);

            if (userInfo != null)
            {
                bool hasAdminRole = userInfo.Roles.Any(x => x.ToLower() == GroupConst.Admin.ToLower());

                if (hasAdminRole == true)
                {
                    HttpContext.Session.SetInt32(SessionConst.IsAdmin, 1);
                }

                //On converti le GUID en string pour la session
                HttpContext.Session.SetString(SessionConst.UserId, userInfo.Sub.ToString());

                //Prenom + Nom
                HttpContext.Session.SetString(SessionConst.FullName, userInfo.Name);

                UserInfo = userInfo;
            }

            return RedirectToPage("/Customs/Index");
        }
    }
}
