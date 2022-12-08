namespace GestionPlacesParking.Core.Models.Locals.History
{
    public class HistoryLocal
    {
        public string Mois { get; set; } = string.Empty;
        public string Trimestre { get; set; } = string.Empty;
        public int Annee { get; set; } = DateTime.Now.Year;
        public double MoyenneReservations { get; set; } = 0;
        public List<HistoryListLocal> HistoryListLocal { get; set; } = new List<HistoryListLocal>();
    }
}
