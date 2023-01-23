namespace GestionPlacesParking.Core.Models.DTOs
{
    public class DeleteOneReservationDto
    {
        public int ReservationId { get; set; }
        public int? UserId { get; set; }
        public string ProprietaireId { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }
}
