using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Models.Locals.HistoryV2
{
    public class HistoryUserLocal
    {
        public string ProprietaireId { get; set; } = string.Empty;
        public int UserId { get; set; } = 0;
        public User User { get; set; } = new User();
        public int NbTotalReservations { get; set; } = 0;
        public string ReservingName { get; set; } = string.Empty;
        public double MoyenneAnnee { get; set; } = 0;
    }
}
