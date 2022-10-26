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

        public List<Reservation> GetAllReserved()
        {
            return Context?.Reservations.Where(r => r.IsDeleted == false).ToList();
        }
    }
}
