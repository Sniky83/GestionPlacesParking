using GestionPlacesParking.Core.Global.Consts;
using GestionPlacesParking.Core.Global.EnvironmentVariables.Envs;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.DTOs;
using KeycloakCore.Keycloak;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs
{
    public class LoginModel : PageModel
    {
        private readonly IUserRepository _repository;

        [BindProperty]
        public new AuthenticationUserDto? User { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(IUserRepository repository, ILogger<LoginModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Connexion via le SSO Keycloak
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGet()
        {
            if (IsSsoEnv.IsSso)
            {
                try
                {
                    var keycloakManager = new WebManager();
                    var url = keycloakManager.GenerateLoginUri();
                    return Redirect(url.ToString());
                }
                catch (Exception ex)
                {
                    _logger.LogCritical("Impossible de se connecter à Keycloak.\n{ex}", ex);
                    return RedirectToAction("CallbackError", new { error = $"Erreur lors de l'appel à Keycloak. Exception {ex.Message}" });
                }
            }

            return Page();
        }

        /// <summary>
        /// Connexion sans SSO
        /// </summary>
        /// <returns></returns>
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
                            HttpContext.Session.SetInt32(SessionConst.IsAdmin, 1);
                        }

                        //Pour que ce soit compatible lors de l'insert de la réservation en BDD
                        HttpContext.Session.SetString(SessionConst.UserId, user.Id.ToString());

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
