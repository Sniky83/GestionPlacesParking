using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionPlacesParking.Core.Interfaces.Infrastructures
{
    public interface IHistoryLocalDataLayer
    {
        List<SelectListItem> ExtractYears();
    }
}
