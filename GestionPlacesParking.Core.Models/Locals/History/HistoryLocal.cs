namespace GestionPlacesParking.Core.Models.Locals.History
{
    public class HistoryLocal
    {
        public enum Choix
        {
            Mois = 1,
            Trimestre = 2,
            Annee = 3,
            MoisEtAnnee = 4
        }
        public string Mois { get; set; } = string.Empty;
        public string Trimestre { get; set; } = string.Empty;
        public int Annee { get; set; } = DateTime.Now.Year;
        public double MoyenneReservations { get; set; } = 0;
        public List<HistoryListLocal> HistoryListLocal { get; set; } = new List<HistoryListLocal>();
    }
}
