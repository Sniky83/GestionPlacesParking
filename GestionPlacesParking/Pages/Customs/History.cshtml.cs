using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Customs
{
    public class HistoriqueModel : PageModel
    {
        private readonly IHistoryLocalRepository _historyLocalRepository;

        [BindProperty]
        public HistoryFilterDto HistoryFilterDto { get; set; }
        public HistoryLocal HistoryLocal { get; set; }

        public HistoriqueModel(IHistoryLocalRepository historyLocalRepository)
        {
            _historyLocalRepository = historyLocalRepository;
            HistoryLocal = new HistoryLocal();
        }

        public IActionResult OnGet()
        {
            HistoryLocal.Years = _historyLocalRepository.GetYears();
            return Page();
        }
    }
}
