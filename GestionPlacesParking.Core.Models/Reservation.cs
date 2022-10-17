using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Models
{
    /// <summary>
    /// Réservation de l'application
    /// </summary>
    public class Reservation
    {
        public int Id { get; set; }
        public int ParkingSlotId { get; set; }
        public ParkingSlot? ParkingSlot { get; set; } = null;
        public string ReservingName { get; set; } = string.Empty;
        public bool IsReserved { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
