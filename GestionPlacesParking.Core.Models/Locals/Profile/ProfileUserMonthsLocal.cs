namespace GestionPlacesParking.Core.Models.Locals.Profile
{
    public class ProfileUserMonthsLocal
    {
        public int Mois { get; set; } = 0;
        public string? MoisString { get; set; } = string.Empty;
        public int NbReservations { get; set; } = 0;
        public double MoyenneMois { get; set; } = 0;
    }
}
