using GestionPlacesParking.Core.Application.Exceptions;
using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals.History;
using KeycloakCore.Keycloak;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace GestionPlacesParking.Core.Application.Repositories
{
    public class HistoryLocalRepository : IHistoryLocalRepository
    {
        private readonly IReservationDataLayer _dataLayer;

        public HistoryLocalRepository(IReservationDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        private string ExtractMonthDisplayName(int month)
        {
            string displayMonth = string.Empty;

            switch (month)
            {
                case 1:
                    displayMonth = "Janvier";
                    break;
                case 2:
                    displayMonth = "Février";
                    break;
                case 3:
                    displayMonth = "Mars";
                    break;
                case 4:
                    displayMonth = "Avril";
                    break;
                case 5:
                    displayMonth = "Mai";
                    break;
                case 6:
                    displayMonth = "Juin";
                    break;
                case 7:
                    displayMonth = "Juillet";
                    break;
                case 8:
                    displayMonth = "Août";
                    break;
                case 9:
                    displayMonth = "Septembre";
                    break;
                case 10:
                    displayMonth = "Octobre";
                    break;
                case 11:
                    displayMonth = "Novembre";
                    break;
                case 12:
                    displayMonth = "Décembre";
                    break;
                default:
                    throw new NotFoundException(nameof(displayMonth));
            }

            return displayMonth;
        }

        private string ExtractQuarterDisplayNameFromMonth(int month)
        {
            string displayQuarter = string.Empty;

            switch (month)
            {
                case <= 3:
                    displayQuarter = "Premier";
                    break;
                case <= 6:
                    displayQuarter = "Second";
                    break;
                case <= 9:
                    displayQuarter = "Troisième";
                    break;
                case <= 12:
                    displayQuarter = "Quatrième";
                    break;
                default:
                    throw new NotFoundException(nameof(displayQuarter));
            }

            return displayQuarter;
        }

        private string ExtractQuarterDisplayNameFromQuarter(int quarter)
        {
            string displayQuarter = string.Empty;

            switch (quarter)
            {
                case 1:
                    displayQuarter = "Premier";
                    break;
                case 2:
                    displayQuarter = "Second";
                    break;
                case 3:
                    displayQuarter = "Troisième";
                    break;
                case 4:
                    displayQuarter = "Quatrième";
                    break;
                default:
                    throw new NotFoundException(nameof(displayQuarter));
            }

            return displayQuarter;
        }

        /// <summary>
        /// Récupère les statistiques des réservations par utilisateur /1 mois donné
        /// Fait également la moyenne des réservations /1 an par utilisateur
        /// Par défaut la fonction prend l'année et le mois courant
        /// </summary>
        /// <param name="historyFilterDto"></param>
        /// <returns></returns>
        public HistoryLocal GetAll(HistoryFilterDto? historyFilterDto = null)
        {
            List<HistoryUserLocal> historyListLocal = _dataLayer.GetNumberReservationsSpecificMonth(historyFilterDto);
            //Sert pour faire la moyenne des réservations /1 an
            List<HistoryUserLocal> userYearReservationList = _dataLayer.GetNumberReservationsSpecificYearForAverage(historyFilterDto);

            List<HistoryUserMonthsLocal> userMonthsReservationList = new List<HistoryUserMonthsLocal>();
            if (historyFilterDto != null && (historyFilterDto.Trimestre >= 1 || historyFilterDto.Annee >= 1))
            {
                userMonthsReservationList = _dataLayer.GetNumberReservationsSpecificTrimesterOrYear(historyFilterDto);
            }

            var webManager = new WebManager();
            var userInfojson = webManager.GetAllUserInfo();

            dynamic jsonObject = JArray.Parse(userInfojson);

            //On prends les données keycloak pour remplir le nom prenom de l'user
            foreach (var oneHistoryLocal in historyListLocal)
            {
                foreach (var jsonObj in jsonObject)
                {
                    string proprietaireId = jsonObj.id;

                    if (oneHistoryLocal.ProprietaireId == proprietaireId)
                    {
                        string fullName = jsonObj.firstName + " " + jsonObj.lastName;
                        oneHistoryLocal.FullName = fullName;
                        break;
                    }
                }

                oneHistoryLocal.MoyenneAnnee = Queryable.Average(
                    userYearReservationList.
                    Where(h => h.ProprietaireId == oneHistoryLocal.ProprietaireId).
                    Select(h => h.NbReservations).AsQueryable()
                );
            }

            int monthCondition = (historyFilterDto == null || historyFilterDto.Mois == 0 ? DateTime.Now.Month : historyFilterDto.Mois);
            int quarterCondition = (historyFilterDto == null || historyFilterDto.Trimestre == 0 ? DateTime.Now.Month : historyFilterDto.Trimestre);
            int yearCondition = (historyFilterDto == null || historyFilterDto.Annee == 0 ? DateTime.Now.Year : historyFilterDto.Annee);

            string mois = ExtractMonthDisplayName(monthCondition);

            string trimestre = (historyFilterDto == null || historyFilterDto.Trimestre == 0 ? ExtractQuarterDisplayNameFromMonth(monthCondition) : ExtractQuarterDisplayNameFromQuarter(historyFilterDto.Trimestre));

            HistoryLocal historyLocal = new HistoryLocal();

            historyLocal.HistoryUserListLocal = historyListLocal;
            historyLocal.Mois = mois;
            historyLocal.Annee = yearCondition;
            historyLocal.Trimestre = trimestre;
            historyLocal.MoyenneReservations = Queryable.Average(historyListLocal.Select(h => h.NbReservations).AsQueryable());

            //Pour récupérer les réservations sur plusieurs mois
            foreach (var oneUserMonthsReservationList in userMonthsReservationList)
            {
                oneUserMonthsReservationList.MoisString = ExtractMonthDisplayName(oneUserMonthsReservationList.Mois);

                historyLocal.HistoryUserMonthsListLocal.Add(oneUserMonthsReservationList);
            }

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
