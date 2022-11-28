namespace GestionPlacesParking.Core.Models.DTOs
{
    public class DeleteOneReservationDto
    {
        public int ReservationId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }
}
