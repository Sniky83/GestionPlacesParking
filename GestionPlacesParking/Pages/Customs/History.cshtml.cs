using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.Locals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Customs
{
    public class HistoriqueModel : PageModel
    {
        private readonly IHistoryLocalRepository _historyLocalRepository;

        [BindProperty]
        public HistoryLocal HistoryLocal { get; set; }

        public HistoriqueModel(IHistoryLocalRepository historyLocalRepository)
        {
            _historyLocalRepository = historyLocalRepository;
        }

        public IActionResult OnGet()
        {
            //HistoryLocal.Years = _historyLocalRepository.GetYears();
            return Page();
        }
    }
}
