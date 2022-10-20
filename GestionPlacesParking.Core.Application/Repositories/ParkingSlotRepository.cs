using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var parkingSlotList = _dataLayer.GetAll();

            if (parkingSlotList == null)
            {
                throw new ArgumentNullException(nameof(parkingSlotList));
            }

            return parkingSlotList;
        }
    }
}
