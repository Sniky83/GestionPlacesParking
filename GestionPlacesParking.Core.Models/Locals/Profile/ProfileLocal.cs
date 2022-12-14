namespace GestionPlacesParking.Core.Models.Locals.Profile
{
    public class ProfileLocal
    {
        public double MaMoyenneAnnee { get; set; } = 0;
        public int TotalReservations { get; set; } = 0;
        public double MoyenneAnnee { get; set; } = 0;
        public List<ProfileUserMonthsLocal> ProfileUserMonthsListLocal { get; set; } = new List<ProfileUserMonthsLocal>();
    }
}
