using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPlacesParking.Core.Models
{
    /// <summary>
    /// Réservation de l'application
    /// </summary>
    public class Reservation
    {
        public int Id { get; set; }
        public int ParkingSlotId { get; set; }
        public ParkingSlot? ParkingSlot { get; set; }
        public bool IsDeleted { get; set; }
        public User? User { get; set; }
        //UserId si IsSSO = 0
        public int UserId { get; set; }
        //ProprietaireId si IsSSO = 1
        public string ProprietaireId { get; set; } = string.Empty;
        [NotMapped]
        public string ReservingName { get; set; } = string.Empty;
        public DateTime ReservationDate { get; set; }
    }
}
