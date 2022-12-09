using GestionPlacesParking.Core.Application.Exceptions;
using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.Locals.History;
using KeycloakCore.Keycloak;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text.Json;
using System.Text.Json.Nodes;

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
            List<HistoryListLocal> historyListLocal = _dataLayer.GetAllCurrentMonth();
            List<HistoryListLocal> historyListSeveralMonthLocal = _dataLayer.GetAllSeveralMonths();

            var webManager = new WebManager();
            var userInfojson = webManager.GetAllUserInfo();

            dynamic jsonObject = JArray.Parse(userInfojson);

            //On prends les données keycloak pour remplir le nom prenom de l'user
            foreach (var oneHistoryLocal in historyListLocal)
            {
                foreach(var jsonObj in jsonObject)
                {
                    string proprietaireId = jsonObj.id;

                    if(oneHistoryLocal.ProprietaireId == proprietaireId)
                    {
                        string fullName = jsonObj.firstName + " " + jsonObj.lastName;
                        oneHistoryLocal.FullName = fullName;
                        break;
                    }
                }

                oneHistoryLocal.MoyenneAnnee = Queryable.Average(
                    historyListSeveralMonthLocal.
                    Where(h => h.ProprietaireId == oneHistoryLocal.ProprietaireId).
                    Select(h => h.NbReservations).AsQueryable()
                );
            }

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

            historyLocal.HistoryListLocal = historyListLocal;
            historyLocal.Mois = mois;
            historyLocal.Annee = DateTime.Now.Year;
            historyLocal.Trimestre = trimestre;
            historyLocal.MoyenneReservations = Queryable.Average(historyListLocal.Select(h => h.NbReservations).AsQueryable());

            return historyLocal;
        }

        public HistoryLocal GetAllSeveralMonths()
        {
            //List<HistoryListLocal> historyListLocal = _dataLayer.GetAllSeveralMonths();

            return new HistoryLocal();
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
