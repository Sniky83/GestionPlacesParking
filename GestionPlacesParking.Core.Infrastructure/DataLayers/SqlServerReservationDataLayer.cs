using GestionPlacesParking.Core.Infrastructure.Databases;
using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;

namespace GestionPlacesParking.Core.Infrastructure.DataLayers
{
    public class SqlServerReservationDataLayer : BaseSqlServerDataLayer, IReservationDataLayer
    {
        public SqlServerReservationDataLayer(ParkingDbContext context) : base(context) { }

        public int AddOne(Reservation reservation)
        {
            var checkDuplicateReservation = Context.Reservations.Where(
                r => r.IsDeleted == false &&
                r.ReservationDate == reservation.ReservationDate &&
                r.ParkingSlotId == reservation.ParkingSlotId
            ).Any();

            //Si la réservation est dupliquée
            if (checkDuplicateReservation)
            {
                return -1;
            }

            Context?.Reservations.Add(reservation);

            return Context.SaveChanges();
        }

        public int DeleteOne(DeleteOneReservationDto deleteOneReservationDto)
        {
            IQueryable<Reservation> findCurrentReservationQuery = null;

            if (deleteOneReservationDto.IsAdmin)
            {
                //Si on est ADMIN on check pas si on est propriétaire de la réservation pour pouvoir la supprimer
                findCurrentReservationQuery = Context.Reservations.Where(r => r.Id == deleteOneReservationDto.ReservationId).AsQueryable();
            }
            else
            {
                //Si on est pas ADMIN seul les propriétaires de la réservations ont le droit à la suppression
                findCurrentReservationQuery = Context.Reservations.Where(r => r.Id == deleteOneReservationDto.ReservationId && r.ProprietaireId == deleteOneReservationDto.UserId).AsQueryable();
            }

            var findCurrentReservation = findCurrentReservationQuery.FirstOrDefault();

            if (findCurrentReservation != null)
            {
                findCurrentReservation.IsDeleted = true;

                return Context.SaveChanges();
            }
            else
            {
                return -1;
            }
        }

        public List<Reservation> GetAllReservationsCurrentWeek()
        {
            //On prends les réservations du lundi jusqu'au vendredi
            DateTime thisMonday = DateTime.Now.AddDays(DayOfWeek.Monday - DateTime.Now.DayOfWeek).Date;
            DateTime thisFriday = thisMonday.AddDays(4).Date;

            return Context?.Reservations.Where(r => r.IsDeleted == false && (r.ReservationDate >= thisMonday && r.ReservationDate <= thisFriday)).ToList();
        }

        public List<Reservation> GetAllReservationsNextWeek()
        {
            //On prends les réservations de la semaine prochaine
            DateTime nextMonday = DateTime.Now.AddDays((DayOfWeek.Monday - DateTime.Now.DayOfWeek) + 7).Date;

            return Context?.Reservations.Where(r => r.IsDeleted == false && r.ReservationDate >= nextMonday).ToList();
        }
    }
}
