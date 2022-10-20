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
    public class SqlServerParkingSlotDataLayer : BaseSqlServerDataLayer, IParkingSlotDataLayer
    {
        public SqlServerParkingSlotDataLayer(ParkingDbContext context) : base(context) { }

        public List<ParkingSlot> GetAll()
        {
            return Context?.ParkingSlots.ToList();
        }
    }
}
