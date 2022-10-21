using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Models.DTOs
{
    public class UserDto
    {
        [Required(ErrorMessage = "Le champ email est obligatoire !")]
        [MaxLength(50, ErrorMessage = "L'email ne doit pas dépasser 50 charactères !")]
        [RegularExpression(@"^\\S+@\\S+\\.\\S+$", ErrorMessage = "Veuillez saisir un email valide !")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le champ mot de passe est obligatoire !")]
        [MaxLength(16, ErrorMessage = "Le mot de passe ne doit pas dépasser 16 charactères !")]
        [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 charactères !")]
        public string Password { get; set; } = string.Empty;
    }
}
