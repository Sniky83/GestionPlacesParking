using System.ComponentModel.DataAnnotations;

namespace GestionPlacesParking.Core.Models
{
    /// <summary>
    /// Utilisateur du site
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }
}
