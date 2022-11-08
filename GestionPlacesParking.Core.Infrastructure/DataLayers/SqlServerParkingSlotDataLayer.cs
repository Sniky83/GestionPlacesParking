using GestionPlacesParking.Core.Infrastructure.Databases;
using GestionPlacesParking.Core.Infrastructure.Web.Cipher;
using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Infrastructure.DataLayers
{
    public class SqlServerReservationDataLayer : BaseSqlServerDataLayer, IReservationDataLayer
    {
        public SqlServerReservationDataLayer(ParkingDbContext context) : base(context) { }

        public int AddOne(Reservation reservation)
        {
            Context?.Reservations.Add(reservation);

            return Context.SaveChanges();
        }

        public List<Reservation> GetAllReserved(DateOnly firstDayOfTheWeek)
        {
            DateTime currentTime = DateTime.Now;

            //Règle métier: Si on est vendredi >= à 11h00
            if ((int)currentTime.DayOfWeek >= 5 && currentTime.Hour >= 11 && currentTime.Minute >= 0)
            {
                DateTime nextMonday = firstDayOfTheWeek.ToDateTime(TimeOnly.Parse("00:00"));

                return Context?.Reservations.Where(r => r.IsDeleted == false && r.ReservationDate >= nextMonday).ToList();
            }
            else
            {
                //On prends les réservations du lundi jusqu'au vendredi
                DateTime thisMonday = firstDayOfTheWeek.AddDays(-7).ToDateTime(TimeOnly.Parse("00:00"));
                DateTime thisFriday = firstDayOfTheWeek.AddDays(-3).ToDateTime(TimeOnly.Parse("00:00"));

                return Context?.Reservations.Where(r => r.IsDeleted == false && (r.ReservationDate >= thisMonday && r.ReservationDate <= thisFriday)).ToList();
            }
        }
    }
}
