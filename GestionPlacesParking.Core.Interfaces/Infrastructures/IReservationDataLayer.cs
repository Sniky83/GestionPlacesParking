using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals.History;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionPlacesParking.Core.Interfaces.Infrastructures
{
    public interface IReservationDataLayer
    {
        List<Reservation> GetAllReservationsCurrentWeek();
        List<Reservation> GetAllReservationsNextWeek();
        int AddOne(Reservation reservation);
        int DeleteOne(DeleteOneReservationDto deleteOneReservationDto);
        List<SelectListItem> ExtractYears();
        List<HistoryListLocal> GetAllCurrentMonth();
        List<HistoryListLocal> GetAllSeveralMonths();
    }
}
