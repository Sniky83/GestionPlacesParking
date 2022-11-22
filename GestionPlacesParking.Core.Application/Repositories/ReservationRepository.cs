﻿using GestionPlacesParking.Core.Application.Exceptions;
using GestionPlacesParking.Core.Global.BusinessLogics;
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

        public int DeleteOne(DeleteOneReservationDto deleteOneReservationDto)
        {
            int deleteOne = _dataLayer.DeleteOne(deleteOneReservationDto);

            //Si on trouve pas la réservation via son ID
            //Ou si la suppression n'a pas marché
            if (deleteOne == 0 || deleteOne == -1)
            {
                throw new NotFoundException(nameof(deleteOne));
            }

            return deleteOne;
        }

        public List<Reservation> GetAll()
        {
            List<Reservation> reservationList;

            if (ReservationBusinessLogic.IsEndReservationsCurrentWeek())
            {
                reservationList = _dataLayer.GetAllReservationsNextWeek();
            }
            else
            {
                reservationList = _dataLayer.GetAllReservationsCurrentWeek();
            }

            return reservationList;
        }

        public int AddOne(Reservation reservation)
        {
            //Si la date n'est pas comprise dans la range de la semaine en cours
            //Ou la semaine prochaine dans le cas échéant
            if (!ReservationBusinessLogic.IsReservationDateInRange(reservation.ReservationDate))
            {
                throw new DateNotInRangeException(nameof(reservation.ReservationDate));
            }

            var insertOne = _dataLayer.AddOne(reservation);

            //Si la reservation est null
            if (insertOne == 0)
            {
                throw new ArgumentNullException(nameof(insertOne));
            }
            else if (insertOne == -1)
            {
                //Si la réservation tente d'être dupliquée
                throw new DuplicateDataException(nameof(insertOne));
            }

            return insertOne;
        }
    }
}
