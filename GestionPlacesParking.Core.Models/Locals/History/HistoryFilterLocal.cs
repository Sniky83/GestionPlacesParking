using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionPlacesParking.Core.Models.Locals.History
{
    public class HistoryFilterLocal
    {
        public enum Mois
        {
            Janvier = 1,
            Février = 2,
            Mars = 3,
            Avril = 4,
            Mai = 5,
            Juin = 6,
            Juillet = 7,
            Août = 8,
            Septembre = 9,
            Octobre = 10,
            Novembre = 11,
            Décembre = 12
        }

        public enum Trimestre
        {
            Premier = 1,
            Second = 2,
            Troisième = 3,
            Quatrième = 4
        }

        public List<SelectListItem> Annee { get; set; }
    }
}
