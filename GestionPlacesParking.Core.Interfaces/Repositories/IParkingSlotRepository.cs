using GestionPlacesParking.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Interfaces.Repositories
{
    public interface IParkingSlotRepository
    {
        List<ParkingSlot> GetAll();
    }
}
