using GestionPlacesParking.Core.Infrastructure.Databases;
using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals.History;
using GestionPlacesParking.Core.Models.Locals.Profile;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionPlacesParking.Core.Infrastructure.DataLayers
{
    public class SqlServerReservationDataLayer : BaseSqlServerDataLayer, IReservationDataLayer
    {
        public SqlServerReservationDataLayer(ParkingDbContext context) : base(context) { }

        public int AddOne(Reservation reservation)
        {
            var checkDuplicateReservation = Context.Reservations
            .Where(
                r => r.IsDeleted == false &&
                r.ReservationDate == reservation.ReservationDate &&
                r.ParkingSlotId == reservation.ParkingSlotId
            ).Any();

            //Si la réservation est dupliquée
            if (checkDuplicateReservation)
            {
                return -1;
            }

            var checkDoubleReservation = Context.Reservations
            .Where(
                r => r.IsDeleted == false &&
                r.ReservationDate == reservation.ReservationDate &&
                r.ProprietaireId == reservation.ProprietaireId
            ).Any();

            //Si le réservataire souhaite réserver 2x le même jour
            if (checkDoubleReservation)
            {
                return -2;
            }

            //Unicité des reserving name
            reservation.ReservingName = reservation.ReservingName.ToUpper().Trim();

            Context.Reservations.Add(reservation);

            return Context.SaveChanges();
        }

        public int DeleteOne(DeleteOneReservationDto deleteOneReservationDto)
        {
            IQueryable<Reservation> findCurrentReservationQuery;

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

            return Context.Reservations.Where(r => r.IsDeleted == false && (r.ReservationDate >= thisMonday && r.ReservationDate <= thisFriday)).ToList();
        }

        public List<Reservation> GetAllReservationsNextWeek()
        {
            //On prends les réservations de la semaine prochaine
            DateTime nextMonday = DateTime.Now.AddDays((DayOfWeek.Monday - DateTime.Now.DayOfWeek) + 7).Date;

            return Context.Reservations.Where(r => r.IsDeleted == false && r.ReservationDate >= nextMonday).ToList();
        }

        public List<SelectListItem> ExtractYears()
        {
            //Récupère la liste des années des réservations
            return Context.Reservations
            .Where(r => r.IsDeleted == false)
            .Select(r =>
                new SelectListItem
                {
                    Value = r.ReservationDate.Year.ToString(),
                    Text = r.ReservationDate.Year.ToString(),
                    Selected = (r.ReservationDate.Year == DateTime.Now.Year ? true : false)
                }
            ).Distinct().ToList();
        }

        public List<HistoryUserLocal> GetNumberReservationsSpecificMonth(FilterHistoryDto historyFilterDto)
        {
            int monthCondition = (historyFilterDto == null || historyFilterDto.Mois == 0 ? DateTime.Now.Month : historyFilterDto.Mois);
            int yearCondition = (historyFilterDto == null || historyFilterDto.Annee == 0 ? DateTime.Now.Year : historyFilterDto.Annee);

            //Récupère le nombre de réservations par personne sur un mois donné
            return Context.Reservations
            .Where(
                r => r.ReservationDate.Month == monthCondition &&
                r.ReservationDate.Year == yearCondition &&
                r.IsDeleted == false)
            .GroupBy(r => new { r.ProprietaireId })
            .Select(h =>
                new HistoryUserLocal
                {
                    ProprietaireId = h.Key.ProprietaireId,
                    NbReservations = h.Count()
                }
            ).ToList();
        }

        public List<HistoryUserMonthsLocal> GetNumberReservationsSpecificTrimesterOrYear(FilterHistoryDto historyFilterDto)
        {
            int startingMonth = 0;

            switch (historyFilterDto.Trimestre)
            {
                case 1:
                    startingMonth = 1;
                    break;
                case 2:
                    startingMonth = 3;
                    break;
                case 3:
                    startingMonth = 6;
                    break;
                case 4:
                    startingMonth = 9;
                    break;
            }

            int endingMonthCondition = (startingMonth == 1 ? startingMonth + 2 : startingMonth + 3);
            endingMonthCondition = (startingMonth == 0 ? 12 : endingMonthCondition);

            int yearCondition = (historyFilterDto.Annee == 0 ? DateTime.Now.Year : historyFilterDto.Annee);

            return Context.Reservations
            .Where(
                r =>
                (
                    //Trimestre
                    r.ReservationDate.Month >= startingMonth &&
                    r.ReservationDate.Month <= endingMonthCondition
                ) &&
                r.ReservationDate.Year == yearCondition &&
                r.IsDeleted == false)
            .GroupBy(r => new { r.ReservationDate.Month, r.ProprietaireId })
            .Select(h =>
                new HistoryUserMonthsLocal
                {
                    ProprietaireId = h.Key.ProprietaireId,
                    NbReservations = h.Count(),
                    Mois = h.Key.Month
                }
            ).ToList();
        }

        public List<HistoryUserLocal> GetNumberReservationsSpecificYearForAverage(FilterHistoryDto historyFilterDto)
        {
            int yearCondition = (historyFilterDto == null || historyFilterDto.Annee == 0 ? DateTime.Now.Year : historyFilterDto.Annee);

            //Récupère le nombre de réservations par personne sur une année
            //Utile également pour calculer la moyenne des réservations /1 année
            return Context.Reservations
            .Where(
                r => r.ReservationDate.Year == yearCondition &&
                r.IsDeleted == false)
            .GroupBy(r => new { r.ReservationDate.Month, r.ProprietaireId })
            .Select(h =>
                new HistoryUserLocal
                {
                    ProprietaireId = h.Key.ProprietaireId,
                    NbReservations = h.Count()
                }
            ).ToList();
        }

        public List<ProfileUserMonthsLocal> GetNumberReservationsThisYear(GetProfileDto getProfileDto)
        {
            return Context.Reservations
            .Where(
                r => r.ReservationDate.Year == DateTime.Now.Year &&
                r.IsDeleted == false &&
                r.ProprietaireId == getProfileDto.UserId)
            .GroupBy(r => new { r.ReservationDate.Month })
            .OrderBy(r => r.Key.Month)
            .Select(p =>
                new ProfileUserMonthsLocal
                {
                    Mois = p.Key.Month,
                    NbReservations = p.Count()
                }
            ).ToList();
        }

        public List<ProfileAllUserMonthsLocal> GetNumberReservationsByMonths()
        {
            return Context.Reservations
            .Where(
                r => r.ReservationDate.Year == DateTime.Now.Year &&
                r.IsDeleted == false)
            .GroupBy(r => new { r.ReservationDate.Month, r.ProprietaireId })
            .OrderBy(r => r.Key.Month)
            .Select(p =>
                new ProfileAllUserMonthsLocal
                {
                    Mois = p.Key.Month,
                    NbReservations = p.Count()
                }
            ).ToList();
        }
    }
}
