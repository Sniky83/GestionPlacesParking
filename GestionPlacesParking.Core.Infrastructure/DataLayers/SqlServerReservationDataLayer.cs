using GestionPlacesParking.Core.Global.EnvironmentVariables.Envs;
using GestionPlacesParking.Core.Global.Utils;
using GestionPlacesParking.Core.Infrastructure.Databases;
using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals.History;
using GestionPlacesParking.Core.Models.Locals.HistoryV2;
using GestionPlacesParking.Core.Models.Locals.Profile;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionPlacesParking.Core.Infrastructure.DataLayers
{
    public class SqlServerReservationDataLayer : BaseSqlServerDataLayer, IReservationDataLayer
    {
        public SqlServerReservationDataLayer(ParkingDbContext context) : base(context) { }

        public int AddOne(Reservation reservation)
        {
            bool checkDuplicateReservation = false;

            if (IsSsoEnv.IsSso)
            {
                checkDuplicateReservation =
                    Context.Reservations
                    .Where(
                        r => r.IsDeleted == false &&
                        r.ReservationDate == reservation.ReservationDate &&
                        r.ParkingSlotId == reservation.ParkingSlotId &&
                        !string.IsNullOrEmpty(r.ProprietaireId)
                    ).Any();
            }
            else
            {
                checkDuplicateReservation =
                    Context.Reservations
                    .Where(
                        r => r.IsDeleted == false &&
                        r.ReservationDate == reservation.ReservationDate &&
                        r.ParkingSlotId == reservation.ParkingSlotId &&
                        r.UserId > 0
                    ).Any();
            }

            //Si la réservation est dupliquée
            if (checkDuplicateReservation)
            {
                return -1;
            }

            bool checkDoubleReservation = false;

            if (IsSsoEnv.IsSso)
            {
                checkDoubleReservation = Context.Reservations
                .Where(
                    r => r.IsDeleted == false &&
                    r.ReservationDate == reservation.ReservationDate &&
                    r.ProprietaireId == reservation.ProprietaireId
                ).Any();
            }
            else
            {
                checkDoubleReservation = Context.Reservations
                .Include(r => r.User)
                .Where(
                    r => r.IsDeleted == false &&
                    r.ReservationDate == reservation.ReservationDate &&
                    r.UserId == reservation.UserId
                ).Any();
            }

            //Si le réservataire souhaite réserver 2x le même jour
            if (checkDoubleReservation)
            {
                return -2;
            }

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
                if (IsSsoEnv.IsSso)
                {
                    //Si on est pas ADMIN seul les propriétaires de la réservations ont le droit à la suppression
                    findCurrentReservationQuery = Context.Reservations.Where(r => r.Id == deleteOneReservationDto.ReservationId && r.ProprietaireId == deleteOneReservationDto.ProprietaireId).AsQueryable();
                }
                else
                {
                    findCurrentReservationQuery = Context.Reservations.Include(r => r.User).Where(r => r.Id == deleteOneReservationDto.ReservationId && r.UserId == deleteOneReservationDto.UserId).AsQueryable();
                }
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

            if (IsSsoEnv.IsSso)
            {
                return Context.Reservations.Where(r => r.IsDeleted == false && !string.IsNullOrEmpty(r.ProprietaireId) && (r.ReservationDate >= thisMonday && r.ReservationDate <= thisFriday)).ToList();
            }
            else
            {
                return Context.Reservations.Include(r => r.User).Where(r => r.IsDeleted == false && r.UserId > 0 && (r.ReservationDate >= thisMonday && r.ReservationDate <= thisFriday)).ToList();
            }
        }

        public List<Reservation> GetAllReservationsNextWeek()
        {
            //On prends les réservations de la semaine prochaine
            DateTime nextMonday = DateTime.Now.AddDays((DayOfWeek.Monday - DateTime.Now.DayOfWeek) + 7).Date;

            if (IsSsoEnv.IsSso)
            {
                return Context.Reservations.Where(r => r.IsDeleted == false && !string.IsNullOrEmpty(r.ProprietaireId) && r.ReservationDate >= nextMonday).ToList();
            }
            else
            {
                return Context.Reservations.Include(r => r.User).Where(r => r.IsDeleted == false && r.UserId > 0 && r.ReservationDate >= nextMonday).ToList();
            }
        }

        public List<SelectListItem> ExtractYears()
        {
            if (IsSsoEnv.IsSso)
            {
                //Récupère la liste des années des réservations
                return Context.Reservations
                .Where(r => !string.IsNullOrEmpty(r.ProprietaireId) && r.IsDeleted == false)
                .Select(r =>
                    new SelectListItem
                    {
                        Value = r.ReservationDate.Year.ToString(),
                        Text = r.ReservationDate.Year.ToString(),
                        Selected = (r.ReservationDate.Year == DateTime.Now.Year ? true : false)
                    }
                ).Distinct().ToList();
            }
            else
            {
                return Context.Reservations
                .Where(r => r.UserId > 0 && r.IsDeleted == false)
                .Select(r =>
                    new SelectListItem
                    {
                        Value = r.ReservationDate.Year.ToString(),
                        Text = r.ReservationDate.Year.ToString(),
                        Selected = (r.ReservationDate.Year == DateTime.Now.Year ? true : false)
                    }
                ).Distinct().ToList();
            }
        }

        public List<ProfileUserMonthsLocal> GetNumberReservationsThisYear(GetProfileDto getProfileDto)
        {
            if (IsSsoEnv.IsSso)
            {
                return Context.Reservations
                .Where(
                    r => r.ReservationDate.Year == DateTime.Now.Year &&
                    r.IsDeleted == false &&
                    r.ProprietaireId == getProfileDto.ProprietaireId)
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
            else
            {
                return Context.Reservations
                .Where(
                    r => r.ReservationDate.Year == DateTime.Now.Year &&
                    r.IsDeleted == false &&
                    r.UserId == getProfileDto.UserId)
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
        }

        public List<ProfileAllUserMonthsLocal> GetNumberReservationsByMonths()
        {
            if (IsSsoEnv.IsSso)
            {
                return Context.Reservations
                .Where(
                    r => r.ReservationDate.Year == DateTime.Now.Year &&
                    !string.IsNullOrEmpty(r.ProprietaireId) &&
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
            else
            {
                return Context.Reservations
                .Where(
                    r => r.ReservationDate.Year == DateTime.Now.Year &&
                    r.UserId > 0 &&
                    r.IsDeleted == false)
                .GroupBy(r => new { r.ReservationDate.Month, r.UserId })
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

        public int GetFirstMonthReserved(int year)
        {
            return Context.Reservations
            .Where(r => r.ReservationDate.Year == year)
            .Select(r => r.ReservationDate.Month)
            .Min();
        }

        public List<HistoryUserLocal> GetNumberReservationsSpecificMonthV2(FilterHistoryDto? filterHistoryDto)
        {
            int monthCondition = (filterHistoryDto == null || filterHistoryDto.Mois == 0 ? DateTime.Now.Month : filterHistoryDto.Mois);
            int yearCondition = (filterHistoryDto == null || filterHistoryDto.Annee == 0 ? DateTime.Now.Year : filterHistoryDto.Annee);

            if (IsSsoEnv.IsSso)
            {
                //Récupère le nombre de réservations par personne sur un mois donné
                return Context.Reservations
                .Where(
                    r => r.ReservationDate.Month == monthCondition &&
                    r.ReservationDate.Year == yearCondition &&
                    !string.IsNullOrEmpty(r.ProprietaireId) &&
                    r.IsDeleted == false)
                .GroupBy(r => new { r.ReservationDate.Month, r.ProprietaireId })
                .Select(h =>
                    new HistoryUserLocal
                    {
                        ProprietaireId = h.Key.ProprietaireId,
                        NbTotalReservations = h.Count()
                    }
                ).ToList();
            }
            else
            {
                return Context.Reservations
                .Include(r => r.User)
                .Where(
                    r => r.ReservationDate.Month == monthCondition &&
                    r.ReservationDate.Year == yearCondition &&
                    r.UserId > 0 &&
                    r.IsDeleted == false)
                .GroupBy(r => new { r.ReservationDate.Month, r.UserId })
                .OrderBy(r => r.Key.Month)
                .Select(h =>
                    new HistoryUserLocal
                    {
                        UserId = h.Key.UserId,
                        NbTotalReservations = h.Count(),
                        User = h.First().User
                    }
                ).ToList();
            }
        }

        public List<HistoryUserQuarterOrYearLocal> GetNumberReservationsSpecificTrimesterWithYearOrYearV2(FilterHistoryDto filterHistoryDto)
        {
            int startingMonth = (filterHistoryDto == null || filterHistoryDto.Trimestre == 0 ? 1 : QuarterUtil.GetStartingMonthFromQuarter(filterHistoryDto.Trimestre));
            int endingMonthCondition = (filterHistoryDto == null || filterHistoryDto.Trimestre == 0 ? 12 : QuarterUtil.GetStartingMonthFromQuarter(filterHistoryDto.Trimestre + 1) - 1);

            int yearCondition = (filterHistoryDto == null || filterHistoryDto.Annee == 0 ? DateTime.Now.Year : filterHistoryDto.Annee);

            if (IsSsoEnv.IsSso)
            {
                return Context.Reservations
                .Where(
                    r =>
                    (
                        //Trimestre
                        r.ReservationDate.Month >= startingMonth &&
                        r.ReservationDate.Month <= endingMonthCondition
                    ) &&
                    r.ReservationDate.Year == yearCondition &&
                    !string.IsNullOrEmpty(r.ProprietaireId) &&
                    r.IsDeleted == false)
                .GroupBy(r => new { r.ReservationDate.Month, r.ProprietaireId })
                .OrderBy(r => r.Key.Month)
                .Select(h =>
                    new HistoryUserQuarterOrYearLocal
                    {
                        ProprietaireId = h.Key.ProprietaireId,
                        NbReservations = h.Count(),
                        Mois = h.Key.Month
                    }
                ).ToList();
            }
            else
            {
                return Context.Reservations
                .Where(
                    r =>
                    (
                        //Trimestre
                        r.ReservationDate.Month >= startingMonth &&
                        r.ReservationDate.Month <= endingMonthCondition
                    ) &&
                    r.ReservationDate.Year == yearCondition &&
                    r.UserId > 0 &&
                    r.IsDeleted == false)
                .GroupBy(r => new { r.ReservationDate.Month, r.UserId })
                .OrderBy(r => r.Key.Month)
                .Select(h =>
                    new HistoryUserQuarterOrYearLocal
                    {
                        UserId = h.Key.UserId,
                        NbReservations = h.Count(),
                        Mois = h.Key.Month
                    }
                ).ToList();
            }
        }

        public List<HistoryUserAvgLocal> GetNumberReservationsSpecificYearForAverageV2(FilterHistoryDto filterHistoryDto)
        {
            int yearCondition = (filterHistoryDto == null || filterHistoryDto.Annee == 0 ? DateTime.Now.Year : filterHistoryDto.Annee);

            if (IsSsoEnv.IsSso)
            {
                //Récupère le nombre de réservations par personne sur une année
                //Utile également pour calculer la moyenne des réservations /1 année
                return Context.Reservations
                .Where(
                    r => r.ReservationDate.Year == yearCondition &&
                    !string.IsNullOrEmpty(r.ProprietaireId) &&
                    r.IsDeleted == false)
                .GroupBy(r => new { r.ReservationDate.Month, r.ProprietaireId })
                .OrderBy(r => r.Key.Month)
                .Select(h =>
                    new HistoryUserAvgLocal
                    {
                        ProprietaireId = h.Key.ProprietaireId,
                        NbReservationsMois = h.Count(),
                        Mois = h.Key.Month
                    }
                ).ToList();
            }
            else
            {
                //Récupère le nombre de réservations par personne sur une année
                //Utile également pour calculer la moyenne des réservations /1 année
                return Context.Reservations
                .Where(
                    r => r.ReservationDate.Year == yearCondition &&
                    r.UserId > 0 &&
                    r.IsDeleted == false)
                .GroupBy(r => new { r.ReservationDate.Month, r.UserId })
                .OrderBy(r => r.Key.Month)
                .Select(h =>
                    new HistoryUserAvgLocal
                    {
                        UserId = h.Key.UserId,
                        NbReservationsMois = h.Count(),
                        Mois = h.Key.Month
                    }
                ).ToList();
            }
        }

        public IEnumerable<int> GetNumberReservationsCurrentMonthForAverage(FilterHistoryDto? filterHistoryDto)
        {
            int monthCondition = (filterHistoryDto == null || filterHistoryDto.Mois == 0 ? DateTime.Now.Month : filterHistoryDto.Mois);
            int yearCondition = (filterHistoryDto == null || filterHistoryDto.Annee == 0 ? DateTime.Now.Year : filterHistoryDto.Annee);

            if (IsSsoEnv.IsSso)
            {
                return Context.Reservations
                .Where(
                    r => r.ReservationDate.Month == monthCondition &&
                    r.ReservationDate.Year == yearCondition &&
                    !string.IsNullOrEmpty(r.ProprietaireId) &&
                    r.IsDeleted == false)
                .GroupBy(r => r.ProprietaireId)
                .Select(h =>
                    h.Count()
                ).ToList();
            }
            else
            {
                return Context.Reservations
                .Where(
                    r => r.ReservationDate.Month == monthCondition &&
                    r.ReservationDate.Year == yearCondition &&
                    r.UserId > 0 &&
                    r.IsDeleted == false)
                .GroupBy(r => r.UserId)
                .Select(h =>
                    h.Count()
                ).ToList();
            }
        }

        public List<HistoryUserLocal> GetUsersWhoReservedSpecificYearV2(FilterHistoryDto filterHistoryDto)
        {
            int yearCondition = (filterHistoryDto == null || filterHistoryDto.Annee == 0 ? DateTime.Now.Year : filterHistoryDto.Annee);

            if (IsSsoEnv.IsSso)
            {
                return Context.Reservations
                .Where(
                    r => r.ReservationDate.Year == yearCondition &&
                    !string.IsNullOrEmpty(r.ProprietaireId) &&
                    r.IsDeleted == false)
                .GroupBy(r => new { r.ProprietaireId })
                .Select(h =>
                    new HistoryUserLocal
                    {
                        ProprietaireId = h.Key.ProprietaireId
                    }
                ).Distinct().ToList();
            }
            else
            {
                return Context.Reservations
                .Include(r => r.User)
                .Where(
                    r => r.ReservationDate.Year == yearCondition &&
                    r.UserId > 0 &&
                    r.IsDeleted == false)
                .GroupBy(r => new { r.UserId })
                .Select(h =>
                    new HistoryUserLocal
                    {
                        UserId = h.Key.UserId,
                        User = h.First().User
                    }
                ).Distinct().ToList();
            }
        }
    }
}
