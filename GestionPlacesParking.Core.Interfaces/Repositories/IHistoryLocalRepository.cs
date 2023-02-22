using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals.History;
using GestionPlacesParking.Core.Models.Locals.HistoryV2;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionPlacesParking.Core.Interfaces.Repositories
{
    public interface IHistoryLocalRepository
    {
        HistoryFilterLocal GetYears();
        HistoryLocal GetAll();
        HistoryLocal GetAllFilter(FilterHistoryDto filterHistoryDto);
    }
}
