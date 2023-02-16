namespace GestionPlacesParking.Core.Models.Locals.HistoryV2
{
    public class HistoryLocal
    {
        public string? Mois { get; set; } = string.Empty;
        public string? Trimestre { get; set; } = string.Empty;
        public int Annee { get; set; } = DateTime.Now.Year;
        public double MoyenneReservationsMois { get; set; } = 0;
        public List<HistoryUserLocal> HistoryUserLocalList { get; set; } = new List<HistoryUserLocal>();
        public List<HistoryUserQuarterOrYearLocal> HistoryUserQuarterOrYearLocalList { get; set; } = new List<HistoryUserQuarterOrYearLocal>();
    }
}
