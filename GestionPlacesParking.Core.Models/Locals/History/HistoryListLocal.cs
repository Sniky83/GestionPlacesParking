namespace GestionPlacesParking.Core.Models.Locals.History
{
    public class HistoryListLocal
    {
        public string Utilisateur { get; set; } = string.Empty;
        //Vide si plusieurs mois
        //Utile si un seul mois
        public int NbReservations { get; set; }
        public double MoyenneAnnee { get; set; }
        //Vide si un seul mois
        //Utile si plusieurs mois
        public List<HistoryMonthsLocal> HistoryMonthsLocal { get; set; } = new List<HistoryMonthsLocal>();
    }
}
