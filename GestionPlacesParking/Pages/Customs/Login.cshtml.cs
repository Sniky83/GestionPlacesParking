using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs
{
    public class LoginModel : PageModel
    {
        private readonly IUserRepository _repository;

        [BindProperty]
        public new UserDto? User { get; set; }
        public string ErrorMessage { get; set; }

        public LoginModel(IUserRepository repository)
        {
            _repository = repository;
        }

        public void OnGet()
        {
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
                        HttpContext.Session.SetString("UserId", user.Email);
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
