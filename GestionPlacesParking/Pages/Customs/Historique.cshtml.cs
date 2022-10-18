using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Customs
{
    public class HistoriqueModel : PageModel
    {
        public IActionResult OnGet()
        {
            //On protège la page si la personne n'est pas ADMIN on renvoit sur l'index
            var isAdmin = HttpContext.Session.GetInt32("IsAdmin");

            if(isAdmin == null || isAdmin == 0)
            {
                return RedirectToPage("/Customs/Index");
            }

            return Page();
        }
    }
}
