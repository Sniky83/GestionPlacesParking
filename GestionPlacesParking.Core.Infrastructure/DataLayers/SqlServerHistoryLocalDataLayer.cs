using GestionPlacesParking.Core.Infrastructure.Databases;
using GestionPlacesParking.Core.Interfaces.Infrastructures;

namespace GestionPlacesParking.Core.Infrastructure.DataLayers
{
    public class SqlServerHistoryLocalDataLayer : BaseSqlServerDataLayer, IHistoryLocalDataLayer
    {
        public SqlServerHistoryLocalDataLayer(ParkingDbContext context) : base(context)
        {
        }

        public List<int> ExtractYears()
        {
            //Récupère la liste des années des réservations
            return Context?.Reservations.Select(r => r.ReservationDate.Year).Distinct().ToList();
        }
    }
}
