using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int deleteReservation = _dataLayer.DeleteOne(deleteOneReservationDto);

            return deleteReservation;
        }

        public List<Reservation> GetAllReserved(DateOnly firstDayOfTheWeek, bool isNextWeek)
        {
            var reservationList = _dataLayer.GetAllReserved(firstDayOfTheWeek, isNextWeek);

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
