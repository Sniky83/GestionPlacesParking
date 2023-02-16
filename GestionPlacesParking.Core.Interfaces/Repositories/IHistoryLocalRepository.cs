using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals.History;
using GestionPlacesParking.Core.Models.Locals.HistoryV2;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionPlacesParking.Core.Interfaces.Repositories
{
    public interface IHistoryLocalRepository
    {
        HistoryFilterLocal GetYears();
        HistoryLocalV1 GetAll(FilterHistoryDto? filterHistoryDto = null);
        HistoryLocal GetAllUserMonth();
        HistoryLocal GetAllFilter(FilterHistoryDto filterHistoryDto);
    }
}
