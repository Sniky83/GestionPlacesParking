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
        List<HistoryUserLocal> GetNumberReservationsSpecificMonth(HistoryFilterDto historyFilterDto);
        List<HistoryUserMonthsLocal> GetNumberReservationsSpecificTrimesterOrYear(HistoryFilterDto historyFilterDto);
        List<HistoryUserLocal> GetNumberReservationsSpecificYearForAverage(HistoryFilterDto historyFilterDto);
    }
}
