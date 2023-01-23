namespace GestionPlacesParking.Core.Models.Locals.History
{
    public class HistoryLocal
    {
        public string Mois { get; set; } = string.Empty;
        public string Trimestre { get; set; } = string.Empty;
        public int Annee { get; set; } = DateTime.Now.Year;
        public double MoyenneReservationsMois { get; set; } = 0;
        public List<HistoryUserLocal> HistoryUserListLocal { get; set; } = new List<HistoryUserLocal>();
        //Vide si un seul mois
        //Utile si plusieurs mois
        public List<HistoryUserMonthsLocal> HistoryUserMonthsListLocal { get; set; } = new List<HistoryUserMonthsLocal>();
    }
}
