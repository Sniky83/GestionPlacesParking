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
            //Sert pour faire la moyenne des réservations /1 an
            List<HistoryUserLocal> userYearOrQuarterReservationList = _dataLayer.GetNumberReservationsSpecificYearForAverage(historyFilterDto);

            List<HistoryUserMonthsLocal> userMonthsReservationList = new List<HistoryUserMonthsLocal>();

            List<HistoryUserLocal> historyUserList = new List<HistoryUserLocal>();

            List<HistoryUserLocal> reservationsByUserList = new List<HistoryUserLocal>();

            HistoryLocal historyLocal = new HistoryLocal();

            //Si on selectionne une année ou un trimestre
            if (historyFilterDto != null && historyFilterDto.Mois == 0 && (historyFilterDto.Trimestre >= 1 || historyFilterDto.Annee >= 1))
            {
                //Récupère les réservations sur un trimestre avec une année selectionnée ou simplement une année
                userMonthsReservationList = _dataLayer.GetNumberReservationsSpecificTrimesterWithYear(historyFilterDto);

                //Récupère la liste des users sur l'année selectionnée
                historyUserList = _dataLayer.GetUsersWhoReservedSpecificYear(historyFilterDto);

                //Récupère les réservations du mois courant pour effectuer ensuite un calcul de moyenne
                reservationsByUserList = _dataLayer.GetNumberReservationsSpecificMonth(historyFilterDto);
            }
            else if (historyFilterDto == null || (historyFilterDto.Mois >= 1 || historyFilterDto.Annee >= 1))
            {
                //Par défaut on prend le mois courant
                historyUserList = _dataLayer.GetNumberReservationsSpecificMonth(historyFilterDto);
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

            int iStart = (historyFilterDto == null || historyFilterDto.Trimestre == 0 ? 1 : QuarterUtil.GetStartingMonthFromQuarter(historyFilterDto.Trimestre));
            int iCondition = (historyFilterDto == null || historyFilterDto.Trimestre == 0 ? 12 : QuarterUtil.GetStartingMonthFromQuarter(historyFilterDto.Trimestre + 1) - 1);

            //On prends les données keycloak pour remplir le nom prenom de l'user
            foreach (var oneHistoryUser in historyUserList.ToList())
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

                //Sert à ajouter les mois vide à 0 pour chaque user
                for (int i = iStart; i <= iCondition; i++)
                {
                    int currentMonth = i;

                    int findMonth = userMonthsReservationList.Where(p => p.Mois == currentMonth && p.ProprietaireId == oneHistoryUser.ProprietaireId).Select(p => p.Mois).FirstOrDefault();

                    if (currentMonth != findMonth)
                    {
                        userMonthsReservationList.Add(new HistoryUserMonthsLocal { ProprietaireId = oneHistoryUser.ProprietaireId, NbReservations = 0, Mois = currentMonth });
                    }
                }

                //Sert au calcul de la Moyenne
                for (int i = 1; i <= 12; i++)
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

                foreach (var oneReservationsByUser in reservationsByUserList)
                {
                    if (oneReservationsByUser.ProprietaireId == oneHistoryUser.ProprietaireId)
                    {
                        oneHistoryUser.NbReservationsMois = oneReservationsByUser.NbReservationsMois;
                    }
                }

                if (historyFilterDto != null && historyFilterDto.Mois == 0 && (historyFilterDto.Trimestre >= 1 || historyFilterDto.Annee >= 1))
                {
                    bool isUser = userMonthsReservationList.Where(u => u.ProprietaireId == oneHistoryUser.ProprietaireId && u.NbReservations > 0).Any();

                    if (!isUser)
                    {
                        historyUserList.Remove(oneHistoryUser);
                    }

                    oneHistoryUser.TotalReservations =
                    userMonthsReservationList
                    .Where(u => u.ProprietaireId == oneHistoryUser.ProprietaireId && u.Mois <= iCondition)
                    .Sum(u => u.NbReservations);
                }
                else
                {
                    oneHistoryUser.TotalReservations =
                    userYearOrQuarterReservationList.
                    Where(h => h.ProprietaireId == oneHistoryUser.ProprietaireId).
                    Sum(h => h.NbReservationsMois);
                }

                oneHistoryUser.MoyenneAnnee = Queryable.Average(nbReservationsMois.AsQueryable());
                //Converti la valeur à 1 chiffre après la virgule
                oneHistoryUser.MoyenneAnnee = Math.Round(oneHistoryUser.MoyenneAnnee, 1);

                nbReservationsMois.Clear();
            }

            foreach (var oneUserMonthsReservationList in userMonthsReservationList)
            {
                oneUserMonthsReservationList.MoisString = Enum.GetName(typeof(Mois), oneUserMonthsReservationList.Mois);
            }

            //Remet les mois dans l'ordre avant la création de l'objet final
            userMonthsReservationList = userMonthsReservationList.OrderBy(p => p.Mois).ToList();
            historyUserList = historyUserList.OrderByDescending(h => h.TotalReservations).ToList();

            historyLocal.Mois = mois;
            historyLocal.Annee = yearCondition;
            historyLocal.Trimestre = trimestre;
            historyLocal.MoyenneReservationsMois = (historyUserList.Count == 0 ? 0 : Math.Round(Queryable.Average(historyUserList.Select(h => h.NbReservationsMois).AsQueryable()), 1));
            historyLocal.HistoryUserListLocal = historyUserList;
            historyLocal.HistoryUserMonthsListLocal = userMonthsReservationList;

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
