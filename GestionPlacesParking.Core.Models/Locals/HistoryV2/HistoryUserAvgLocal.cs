using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Models.Locals.HistoryV2
{
    public class HistoryUserAvgLocal
    {
        public string ProprietaireId { get; set; } = string.Empty;
        public int UserId { get; set; } = 0;
        public int NbReservationsMois { get; set; } = 0;
        public int Mois { get; set; } = 0;
    }
}
