using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            //On redirige vers le vrai index
            return RedirectToPage("/Customs/Index");
        }
    }
}
