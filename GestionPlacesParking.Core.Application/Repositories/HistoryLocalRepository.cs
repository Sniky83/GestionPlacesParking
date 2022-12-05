using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionPlacesParking.Core.Application.Repositories
{
    public class HistoryLocalRepository : IHistoryLocalRepository
    {
        private readonly IHistoryLocalDataLayer _dataLayer;

        public HistoryLocalRepository(IHistoryLocalDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public List<SelectListItem> GetYears()
        {
            List<SelectListItem> yearsList = _dataLayer.ExtractYears();

            if (yearsList.Count == 0)
            {
                //Aucune réservation n'est enregistré en BDD
                throw new ArgumentNullException(nameof(yearsList));
            }

            return yearsList;
        }
    }
}
