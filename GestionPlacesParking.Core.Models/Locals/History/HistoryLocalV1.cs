namespace GestionPlacesParking.Core.Models.Locals.History
{
    public class HistoryLocalV1
    {
        public string Mois { get; set; } = string.Empty;
        public string Trimestre { get; set; } = string.Empty;
        public int Annee { get; set; } = DateTime.Now.Year;
        public double MoyenneReservationsMois { get; set; } = 0;
        public List<HistoryUserLocalV1> HistoryUserListLocal { get; set; } = new List<HistoryUserLocalV1>();
        //Vide si un seul mois
        //Utile si plusieurs mois
        public List<HistoryUserMonthsLocal> HistoryUserMonthsListLocal { get; set; } = new List<HistoryUserMonthsLocal>();
    }
}
