using System.ComponentModel.DataAnnotations;

namespace GestionPlacesParking.Core.Models.DTOs
{
    public class AuthenticationUserDto
    {
        [Required(ErrorMessage = "Le champ email est obligatoire !")]
        [MaxLength(50, ErrorMessage = "L'email ne doit pas dépasser 50 caractères !")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le champ mot de passe est obligatoire !")]
        [MaxLength(16, ErrorMessage = "Le mot de passe ne doit pas dépasser 16 caractères !")]
        [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères !")]
        public string Password { get; set; } = string.Empty;
    }
}
