using GestionPlacesParking.Core.Global.Utils;
using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals.History;
using KeycloakCore.Keycloak;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using static GestionPlacesParking.Core.Models.Locals.History.HistoryFilterLocal;

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

            HistoryLocal historyLocal = new HistoryLocal();

            if (historyFilterDto != null && (historyFilterDto.Trimestre >= 1 || historyFilterDto.Annee >= 1))
            {
                userMonthsReservationList = _dataLayer.GetNumberReservationsSpecificTrimesterWithYear(historyFilterDto);

                int iStart = QuarterUtils.GetStartingMonthFromQuarter(historyFilterDto.Trimestre);
                int iCondition = (historyFilterDto.Trimestre * 3) - 1;

                //On parcours tous les mois de l'année
                for (int i = iStart; i < iCondition; i++)
                {
                    int currentMonth = (i + 1);

                    int findMonth = userMonthsReservationList.Where(p => p.Mois == currentMonth).Select(p => p.Mois).FirstOrDefault();

                    if (currentMonth != findMonth)
                    {
                        userMonthsReservationList.Add(new HistoryUserMonthsLocal { NbReservations = 0, Mois = currentMonth });
                    }
                }

                userMonthsReservationList = userMonthsReservationList.OrderBy(p => p.Mois).ToList();
            }

            //Pour récupérer les réservations sur plusieurs mois
            foreach (var oneUserMonthsReservationList in userMonthsReservationList)
            {
                oneUserMonthsReservationList.MoisString = Enum.GetName(typeof(Mois), oneUserMonthsReservationList.Mois);

                //Alimente l'history local
                historyLocal.HistoryUserMonthsListLocal.Add(oneUserMonthsReservationList);
            }

            int monthCondition = (historyFilterDto == null || historyFilterDto.Mois == 0 ? DateTime.Now.Month : historyFilterDto.Mois);
            int quarterCondition = (historyFilterDto == null || historyFilterDto.Trimestre == 0 ? DateTime.Now.Month : historyFilterDto.Trimestre);
            int yearCondition = (historyFilterDto == null || historyFilterDto.Annee == 0 ? DateTime.Now.Year : historyFilterDto.Annee);

            string? mois = Enum.GetName(typeof(Mois), monthCondition);

            string? trimestre = (historyFilterDto == null || historyFilterDto.Trimestre == 0 ? "" : Enum.GetName(typeof(Trimestre), historyFilterDto.Trimestre));

            var webManager = new WebManager();
            var userInfojson = webManager.GetAllUserInfo();

            dynamic jsonObject = JArray.Parse(userInfojson);

            int moisStartMoyenne = _dataLayer.GetFirstMonthReserved(yearCondition);

            List<int> nbReservationsMois = new List<int>();

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

                //Sert au calcul de la Moyenne
                for (int i = 1; i < 13; i++)
                {
                    if (i < moisStartMoyenne)
                    {
                        continue;
                    }

                    var findMonth = userYearOrQuarterReservationList.Where(p => p.Mois == i && p.ProprietaireId == oneHistoryUser.ProprietaireId).Select(p => new { p.NbReservationsMois, p.Mois }).FirstOrDefault();

                    if (findMonth?.Mois == i)
                    {
                        nbReservationsMois.Add(findMonth.NbReservationsMois);
                    }
                    else
                    {
                        nbReservationsMois.Add(0);
                    }
                }

                oneHistoryUser.MoyenneAnnee = Queryable.Average(nbReservationsMois.AsQueryable());

                //Converti la valeur à 1 chiffre après la virgule
                oneHistoryUser.MoyenneAnnee = Math.Round(oneHistoryUser.MoyenneAnnee, 1);

                nbReservationsMois.Clear();
            }

            foreach (var oneHistoryUserLocal in historyUserList)
            {
                oneHistoryUserLocal.TotalReservations = Queryable.Sum(
                    userYearOrQuarterReservationList.
                    Where(h => h.ProprietaireId == oneHistoryUserLocal.ProprietaireId).
                    Select(h => h.NbReservationsMois).AsQueryable()
                );
            }

            historyLocal.Mois = mois;
            historyLocal.Annee = yearCondition;
            historyLocal.Trimestre = trimestre;
            historyLocal.MoyenneReservations = (historyUserList.Count == 0 ? 0 : Math.Round(Queryable.Average(historyUserList.Select(h => h.NbReservationsMois).AsQueryable()), 1));
            historyLocal.HistoryUserListLocal = historyUserList;

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
