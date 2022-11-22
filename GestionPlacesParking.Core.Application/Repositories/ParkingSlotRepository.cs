using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models;

namespace GestionPlacesParking.Core.Application.Repositories
{
    public class ParkingSlotRepository : IParkingSlotRepository
    {
        private readonly IParkingSlotDataLayer _dataLayer;

        public ParkingSlotRepository(IParkingSlotDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public List<ParkingSlot> GetAll()
        {
            return _dataLayer.GetAll();
        }
    }
}
