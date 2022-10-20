using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Models.DTOs
{
    public class ReservationDto
    {
        public int ParkingSlotId { get; set; }
        public string ReservingName { get; set; } = string.Empty;
        public bool IsReserved { get; set; } = true;
        public DateTime ReservationDate { get; set; }
    }
}
