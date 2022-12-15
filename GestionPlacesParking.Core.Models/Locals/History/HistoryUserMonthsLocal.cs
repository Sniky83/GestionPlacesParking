namespace GestionPlacesParking.Core.Models.Locals.History
{
    public class HistoryUserMonthsLocal
    {
        public string ProprietaireId { get; set; } = string.Empty;
        public int Mois { get; set; } = 0;
        public string MoisString { get; set; } = string.Empty;
        public int NbReservations { get; set; } = 0;
    }
}
