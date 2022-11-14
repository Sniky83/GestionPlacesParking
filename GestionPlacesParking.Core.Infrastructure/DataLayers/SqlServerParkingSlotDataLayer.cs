using GestionPlacesParking.Core.Infrastructure.Databases;
using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Models;

namespace GestionPlacesParking.Core.Infrastructure.DataLayers
{
    public class SqlServerParkingSlotDataLayer : BaseSqlServerDataLayer, IParkingSlotDataLayer
    {
        public SqlServerParkingSlotDataLayer(ParkingDbContext context) : base(context) { }

        public List<ParkingSlot> GetAll()
        {
            return Context?.ParkingSlots.ToList();
        }
    }
}
