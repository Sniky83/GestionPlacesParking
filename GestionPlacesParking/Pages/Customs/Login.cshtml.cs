using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs
{
    public class LoginModel : PageModel
    {
        private readonly IUserRepository _repository;

        [BindProperty]
        public new AuthenticationUserDto? User { get; set; }
        public string ErrorMessage { get; set; }

        public LoginModel(IUserRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnPost()
        {
            IActionResult result = Page();

            //Si les data annotations sont valides
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _repository.LogIn(User);

                    if (user != null)
                    {
                        if (user.IsAdmin == true)
                        {
                            HttpContext.Session.SetInt32("IsAdmin", 1);
                        }

                        HttpContext.Session.SetInt32("UserId", user.Id);
                        result = RedirectToPage("/Index");
                    }
                }
                catch (Exception)
                {
                    ErrorMessage = "Mot de passe ou adresse e-mail incorrect !";
                }
            }

            return result;
        }
    }
}
