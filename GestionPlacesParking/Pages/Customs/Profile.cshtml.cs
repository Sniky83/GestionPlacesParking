using GestionPlacesParking.Core.Global.Consts;
using GestionPlacesParking.Core.Global.EnvironmentVariables.Envs;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals.Profile;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs
{
    public class ProfileModel : PageModel
    {
        private readonly IProfileLocalRepository _profileLocalRepository;

        [BindProperty]
        public ProfileLocal ProfileLocal { get; set; }
        public string ErrorMessage { get; set; }

        public ProfileModel(IProfileLocalRepository profileLocalRepository)
        {
            _profileLocalRepository = profileLocalRepository;
        }

        public IActionResult OnGet()
        {
            try
            {
                GetProfileDto getProfileDto = new GetProfileDto();

                if (IsSsoEnv.IsSso)
                {
                    getProfileDto.ProprietaireId = HttpContext.Session.GetString(SessionConst.UserId);
                }
                else
                {
                    getProfileDto.UserId = HttpContext.Session.GetInt32(SessionConst.UserId);
                }

                ProfileLocal = _profileLocalRepository.GetAll(getProfileDto);
            }
            catch (Exception)
            {
                ErrorMessage = "Aucune réservation n'est enregistrée dans la base de données.";
            }

            return Page();
        }
    }
}
