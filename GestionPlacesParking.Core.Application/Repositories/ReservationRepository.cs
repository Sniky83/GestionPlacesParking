using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;

namespace GestionPlacesParking.Core.Application.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IReservationDataLayer _dataLayer;

        public ReservationRepository(IReservationDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public int DeleteReservation(DeleteOneReservationDto deleteOneReservationDto)
        {
            return _dataLayer.DeleteOne(deleteOneReservationDto);
        }

        public List<Reservation> GetAllReserved()
        {
            var reservationList = _dataLayer.GetAllReserved();

            if (reservationList == null)
            {
                throw new ArgumentNullException(nameof(reservationList));
            }

            return reservationList;
        }

        public int MakeReservation(Reservation reservation)
        {
            return _dataLayer.AddOne(reservation);
        }
    }
}
