using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals.History;
using GestionPlacesParking.Core.Models.Locals.HistoryV2;
using GestionPlacesParking.Core.Models.Locals.Profile;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionPlacesParking.Core.Interfaces.Infrastructures
{
    public interface IReservationDataLayer
    {
        //Reservation Repository
        List<Reservation> GetAllReservationsCurrentWeek();
        List<Reservation> GetAllReservationsNextWeek();
        int AddOne(Reservation reservation);
        int DeleteOne(DeleteOneReservationDto deleteOneReservationDto);
        List<SelectListItem> ExtractYears();

        //History Repository
        List<HistoryUserLocal> GetNumberReservationsSpecificMonthV2(FilterHistoryDto? filterHistoryDto = null);
        List<HistoryUserQuarterOrYearLocal> GetNumberReservationsSpecificTrimesterWithYearOrYearV2(FilterHistoryDto filterHistoryDto);
        List<HistoryUserAvgLocal> GetNumberReservationsSpecificYearForAverageV2(FilterHistoryDto? filterHistoryDto = null);
        List<HistoryUserLocal> GetUsersWhoReservedSpecificYearV2(FilterHistoryDto filterHistoryDto);
        IEnumerable<int> GetNumberReservationsCurrentMonthForAverage(FilterHistoryDto? filterHistoryDto = null);

        //Profile Repository
        List<ProfileUserMonthsLocal> GetNumberReservationsThisYear(GetProfileDto profileDto);
        List<ProfileAllUserMonthsLocal> GetNumberReservationsByMonths();
        int GetFirstMonthReserved(int year);
    }
}
