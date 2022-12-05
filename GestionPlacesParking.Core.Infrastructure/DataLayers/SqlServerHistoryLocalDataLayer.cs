using GestionPlacesParking.Core.Infrastructure.Databases;
using GestionPlacesParking.Core.Interfaces.Infrastructures;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionPlacesParking.Core.Infrastructure.DataLayers
{
    public class SqlServerHistoryLocalDataLayer : BaseSqlServerDataLayer, IHistoryLocalDataLayer
    {
        public SqlServerHistoryLocalDataLayer(ParkingDbContext context) : base(context)
        {
        }

        public List<SelectListItem> ExtractYears()
        {
            //Récupère la liste des années des réservations
            return Context?.Reservations.Select(r =>
            new SelectListItem
            {
                Value = r.ReservationDate.Year.ToString(),
                Text = r.ReservationDate.Year.ToString()
            }).Distinct().ToList();
        }
    }
}
