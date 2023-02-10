using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals.History;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Customs
{
    public class HistoriqueModel : PageModel
    {
        private readonly ILogger<HistoriqueModel> _logger;
        private readonly IHistoryLocalRepository _historyLocalRepository;

        [BindProperty]
        public FilterHistoryDto FilterHistoryDto { get; set; }
        public HistoryFilterLocal HistoryFilterLocal { get; set; }
        public HistoryLocalV1 HistoryLocal { get; set; }
        public string ErrorMessage { get; set; }

        public HistoriqueModel(ILogger<HistoriqueModel> logger, IHistoryLocalRepository historyLocalRepository)
        {
            _historyLocalRepository = historyLocalRepository;
            _logger = logger;
            HistoryFilterLocal = new HistoryFilterLocal();
        }

        public IActionResult OnGet()
        {
            try
            {
                HistoryFilterLocal = _historyLocalRepository.GetYears();
                HistoryLocal = _historyLocalRepository.GetAll();
                _historyLocalRepository.GetAllUserMonth();
            }
            catch (Exception)
            {
                ErrorMessage = "Aucune réservation n'est enregistrée dans la base de données.";
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            IActionResult result = Page();

            try
            {
                HistoryFilterLocal = _historyLocalRepository.GetYears();

                _historyLocalRepository.GetAllFilter(FilterHistoryDto);

                if (FilterHistoryDto != null && (FilterHistoryDto.Mois >= 1 || FilterHistoryDto.Trimestre >= 1 || FilterHistoryDto.Annee >= 1))
                {
                    HistoryLocal = _historyLocalRepository.GetAll(FilterHistoryDto);
                }
                else
                {
                    //Get current Month
                    HistoryLocal = _historyLocalRepository.GetAll();
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError);
                _logger.LogCritical("Erreur interne avec l'HistoryLocal repository.\n{ex}", ex);
            }

            return result;
        }
    }
}
