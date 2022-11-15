namespace GestionPlacesParking.Core.Models
{
    /// <summary>
    /// Référencie tous les ADMIN de l'application
    /// </summary>
    public class Admin
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
