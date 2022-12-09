using GestionPlacesParking.Core.Models.Locals.History;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionPlacesParking.Core.Interfaces.Repositories
{
    public interface IHistoryLocalRepository
    {
        List<SelectListItem> GetYears();
        HistoryLocal GetAllCurrentMonth();
        HistoryLocal GetAllSeveralMonths();
    }
}
