using System.ComponentModel.DataAnnotations;

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
        [MaxLength(50, ErrorMessage = "Le champ Nom doit contenir au maximum 50 caractères.")]
        [MinLength(3, ErrorMessage = "Le champ Nom doit contenir au moins 3 caractères.")]
        [RegularExpression(@"^[^\p{P}\p{Sm}0-9]*$", ErrorMessage = "Les caractères spéciaux et les chiffres sont interdits.")]
        public string ReservingName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        //Ne fait référence a aucune table interne puisque la donnée provient de Keycloak
        public string ProprietaireId { get; set; } = string.Empty;
        public DateTime ReservationDate { get; set; }
    }
}
