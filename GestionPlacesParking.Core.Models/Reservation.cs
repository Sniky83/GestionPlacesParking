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
        public string ProprietaireId { get; set; } = string.Empty;
        public DateTime ReservationDate { get; set; }
    }
}
