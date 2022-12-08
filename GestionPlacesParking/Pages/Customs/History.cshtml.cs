using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals.History;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Customs
{
    public class HistoriqueModel : PageModel
    {
        private readonly IHistoryLocalRepository _historyLocalRepository;

        [BindProperty]
        public HistoryFilterDto HistoryFilterDto { get; set; }
        public HistoryFilterLocal HistoryFilterLocal { get; set; }
        public HistoryLocal HistoryLocal { get; set; }
        public string ErrorMessage { get; set; }

        public HistoriqueModel(IHistoryLocalRepository historyLocalRepository)
        {
            _historyLocalRepository = historyLocalRepository;
            HistoryFilterLocal = new HistoryFilterLocal();
        }

        public IActionResult OnGet()
        {
            try
            {
                HistoryFilterLocal.Annee = _historyLocalRepository.GetYears();
                HistoryLocal = _historyLocalRepository.GetAllCurrentMonth();
            }
            catch (Exception)
            {
                ErrorMessage = "Aucune réservation n'est enregistrée dans la base de données.";
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {

            }
            catch (Exception)
            {
                ErrorMessage = "Veuillez sélectionner un filtre existant.";
            }
            return Page();
        }
    }
}
