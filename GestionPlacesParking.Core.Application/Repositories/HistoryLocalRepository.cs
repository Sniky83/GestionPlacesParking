using GestionPlacesParking.Core.Application.Utils;
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

        /// <summary>
        /// Récupère les statistiques des réservations par utilisateur /1 mois donné
        /// Fait également la moyenne des réservations /1 an par utilisateur
        /// Par défaut la fonction prend l'année et le mois courant
        /// </summary>
        /// <param name="historyFilterDto"></param>
        /// <returns></returns>
        public HistoryLocal GetAll(FilterHistoryDto? historyFilterDto = null)
        {
            List<HistoryUserLocal> historyUserList = _dataLayer.GetNumberReservationsSpecificMonth(historyFilterDto);
            //Sert pour faire la moyenne des réservations /1 an
            List<HistoryUserLocal> userYearOrQuarterReservationList = _dataLayer.GetNumberReservationsSpecificYearForAverage(historyFilterDto);

            List<HistoryUserMonthsLocal> userMonthsReservationList = new List<HistoryUserMonthsLocal>();
            if (historyFilterDto != null && (historyFilterDto.Trimestre >= 1 || historyFilterDto.Annee >= 1))
            {
                userMonthsReservationList = _dataLayer.GetNumberReservationsSpecificTrimesterOrYear(historyFilterDto);
            }

            var webManager = new WebManager();
            var userInfojson = webManager.GetAllUserInfo();

            dynamic jsonObject = JArray.Parse(userInfojson);

            //On prends les données keycloak pour remplir le nom prenom de l'user
            foreach (var oneHistoryUser in historyUserList)
            {
                foreach (var jsonObj in jsonObject)
                {
                    string proprietaireId = jsonObj.id;

                    if (oneHistoryUser.ProprietaireId == proprietaireId)
                    {
                        string fullName = jsonObj.firstName + " " + jsonObj.lastName;
                        oneHistoryUser.FullName = fullName;
                        break;
                    }
                }

                oneHistoryUser.MoyenneAnnee = Queryable.Average(
                    userYearOrQuarterReservationList.
                    Where(h => h.ProprietaireId == oneHistoryUser.ProprietaireId).
                    Select(h => h.NbReservations).AsQueryable()
                );
            }

            int monthCondition = (historyFilterDto == null || historyFilterDto.Mois == 0 ? DateTime.Now.Month : historyFilterDto.Mois);
            int quarterCondition = (historyFilterDto == null || historyFilterDto.Trimestre == 0 ? DateTime.Now.Month : historyFilterDto.Trimestre);
            int yearCondition = (historyFilterDto == null || historyFilterDto.Annee == 0 ? DateTime.Now.Year : historyFilterDto.Annee);

            string mois = DisplayNameUtil.GetMonthDisplayNameByMonth(monthCondition);

            string trimestre = (historyFilterDto == null || historyFilterDto.Trimestre == 0 ? DisplayNameUtil.GetQuarterDisplayNameByMonth(monthCondition) : DisplayNameUtil.GetQuarterDisplayNameByQuarter(historyFilterDto.Trimestre));

            HistoryLocal historyLocal = new HistoryLocal();

            foreach (var oneHistoryUserLocal in historyUserList)
            {
                oneHistoryUserLocal.TotalReservations = Queryable.Sum(
                    userYearOrQuarterReservationList.
                    Where(h => h.ProprietaireId == oneHistoryUserLocal.ProprietaireId).
                    Select(h => h.NbReservations).AsQueryable()
                );
            }

            historyLocal.HistoryUserListLocal = historyUserList;
            historyLocal.Mois = mois;
            historyLocal.Annee = yearCondition;
            historyLocal.Trimestre = trimestre;
            historyLocal.MoyenneReservations = Queryable.Average(historyUserList.Select(h => h.NbReservations).AsQueryable());

            //Pour récupérer les réservations sur plusieurs mois
            foreach (var oneUserMonthsReservationList in userMonthsReservationList)
            {
                oneUserMonthsReservationList.MoisString = DisplayNameUtil.GetMonthDisplayNameByMonth(oneUserMonthsReservationList.Mois);

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
