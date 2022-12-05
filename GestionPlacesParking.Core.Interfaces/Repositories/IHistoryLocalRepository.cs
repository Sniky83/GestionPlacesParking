using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionPlacesParking.Core.Interfaces.Repositories
{
    public interface IHistoryLocalRepository
    {
        List<SelectListItem> GetYears();
    }
}
