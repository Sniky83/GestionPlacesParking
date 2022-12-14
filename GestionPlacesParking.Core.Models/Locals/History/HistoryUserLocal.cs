namespace GestionPlacesParking.Core.Models.Locals.History
{
    public class HistoryUserLocal
    {
        public string ProprietaireId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        //Vide si plusieurs mois
        //Utile si un seul mois
        public int NbReservations { get; set; }
        public double MoyenneAnnee { get; set; }
        public int TotalReservations { get; set; }
    }
}
