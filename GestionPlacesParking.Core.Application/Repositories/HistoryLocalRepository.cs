using GestionPlacesParking.Core.Application.Exceptions;
using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.Locals.History;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionPlacesParking.Core.Application.Repositories
{
    public class HistoryLocalRepository : IHistoryLocalRepository
    {
        private readonly IReservationDataLayer _dataLayer;

        public HistoryLocalRepository(IReservationDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public HistoryLocal GetAllCurrentMonth()
        {
            List<HistoryListLocal> historyLocalList = _dataLayer.GetAllCurrentMonth();

            string trimestre = string.Empty;

            switch (DateTime.Now.Month)
            {
                case <= 3:
                    trimestre = "Premier";
                    break;
                case <= 6:
                    trimestre = "Second";
                    break;
                case <= 9:
                    trimestre = "Troisième";
                    break;
                case <= 12:
                    trimestre = "Quatrième";
                    break;
                default:
                    throw new NotFoundException(nameof(trimestre));
            }

            string mois = string.Empty;

            switch (DateTime.Now.Month)
            {
                case 1:
                    mois = "Janvier";
                    break;
                case 2:
                    mois = "Février";
                    break;
                case 3:
                    mois = "Mars";
                    break;
                case 4:
                    mois = "Avril";
                    break;
                case 5:
                    mois = "Mai";
                    break;
                case 6:
                    mois = "Juin";
                    break;
                case 7:
                    mois = "Juillet";
                    break;
                case 8:
                    mois = "Août";
                    break;
                case 9:
                    mois = "Septembre";
                    break;
                case 10:
                    mois = "Octobre";
                    break;
                case 11:
                    mois = "Novembre";
                    break;
                case 12:
                    mois = "Décembre";
                    break;
                default:
                    throw new NotFoundException(nameof(mois));
            }

            HistoryLocal historyLocal = new HistoryLocal();

            historyLocal.HistoryListLocal = historyLocalList;
            historyLocal.Mois = mois;
            historyLocal.Annee = DateTime.Now.Year;
            historyLocal.Trimestre = trimestre;
            historyLocal.MoyenneReservations = Queryable.Average(historyLocalList.Select(h => h.NbReservations).AsQueryable());

            return historyLocal;
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
