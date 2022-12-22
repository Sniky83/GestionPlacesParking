using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals.History;
using GestionPlacesParking.Core.Models.Locals.Profile;
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
        List<HistoryUserLocal> GetNumberReservationsSpecificMonth(FilterHistoryDto historyFilterDto);
        List<HistoryUserMonthsLocal> GetNumberReservationsSpecificTrimesterWithYear(FilterHistoryDto historyFilterDto);
        List<HistoryUserLocal> GetNumberReservationsSpecificYearForAverage(FilterHistoryDto historyFilterDto);
        List<ProfileUserMonthsLocal> GetNumberReservationsThisYear(GetProfileDto profileDto);
        List<ProfileAllUserMonthsLocal> GetNumberReservationsByMonths();
        public int GetFirstMonthReserved(int year);
    }
}
