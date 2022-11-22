using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;

namespace GestionPlacesParking.Core.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        List<Reservation> GetAll();
        int AddOne(Reservation reservation);
        int DeleteOne(DeleteOneReservationDto deleteOneReservationDto);
    }
}
