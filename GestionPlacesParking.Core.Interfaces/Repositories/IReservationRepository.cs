using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;

namespace GestionPlacesParking.Core.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        List<Reservation> GetAllReserved();
        int MakeReservation(Reservation reservation);
        int DeleteReservation(DeleteOneReservationDto deleteOneReservationDto);
    }
}
