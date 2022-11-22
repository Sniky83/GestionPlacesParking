using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;

namespace GestionPlacesParking.Core.Interfaces.Infrastructures
{
    public interface IReservationDataLayer
    {
        List<Reservation> GetAllReservationsCurrentWeek();
        List<Reservation> GetAllReservationsNextWeek();
        int AddOne(Reservation reservation);
        int DeleteOne(DeleteOneReservationDto deleteOneReservationDto);
    }
}
