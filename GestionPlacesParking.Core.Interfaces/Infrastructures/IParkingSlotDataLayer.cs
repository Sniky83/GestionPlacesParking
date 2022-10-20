using GestionPlacesParking.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Interfaces.Infrastructures
{
    public interface IParkingSlotDataLayer
    {
        List<ParkingSlot> GetAll();
    }
}
