namespace GestionPlacesParking.Core.Models.DTOs
{
    public class GetProfileDto
    {
        public int? UserId { get; set; }
        public string ProprietaireId { get; set; } = string.Empty;
    }
}
