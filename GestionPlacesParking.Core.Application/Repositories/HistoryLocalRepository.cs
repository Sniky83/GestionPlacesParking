using GestionPlacesParking.Core.Application.Exceptions;
using GestionPlacesParking.Core.Application.Utils;
using GestionPlacesParking.Core.Global.EnvironmentVariables.Envs;
using GestionPlacesParking.Core.Global.Utils;
using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals.History;
using GestionPlacesParking.Core.Models.Locals.HistoryV2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
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
        
        #region Generic Methods
        private HistoryLocal FillHistoryLocal(FilterHistoryDto filterHistoryDto = null, List<HistoryUserLocal> historyUserLocalList = null, List<HistoryUserQuarterOrYearLocal> historyUserQuarterOrYearLocalList = null)
        {
            FillYearAverage(filterHistoryDto, historyUserLocalList);

            if (historyUserLocalList != null)
            {
                ReservationUtil.FillAllReservingName(historyUserLocalList);
            }
            else if(historyUserQuarterOrYearLocalList != null)
            {
                ReservationUtil.FillAllReservingName(historyUserQuarterOrYearLocalList);
            }

            string? mois = Enum.GetName(typeof(Mois), (filterHistoryDto == null || filterHistoryDto.Mois == 0 ? DateTime.Now.Month : filterHistoryDto.Mois));
            string? trimestre = (filterHistoryDto == null || filterHistoryDto.Trimestre == 0 ? string.Empty : Enum.GetName(typeof(Trimestre), filterHistoryDto.Trimestre));
            int annee = (filterHistoryDto == null || filterHistoryDto.Annee == 0 ? DateTime.Now.Year : filterHistoryDto.Annee);

            var moyenneMensuelle = _dataLayer.GetNumberReservationsCurrentMonthForAverage(filterHistoryDto);

            HistoryLocal historyLocalBase = new HistoryLocal();

            historyLocalBase.Mois = mois;
            historyLocalBase.Trimestre = trimestre;
            historyLocalBase.Annee = annee;

            if(moyenneMensuelle.Any())
                historyLocalBase.MoyenneReservationsMois = Math.Round(moyenneMensuelle.Average(), 1, MidpointRounding.AwayFromZero);

            if(historyUserLocalList != null)
                //On affiche la liste par ordre de réservations
                historyLocalBase.HistoryUserLocalList = historyUserLocalList.OrderByDescending(h => h.NbTotalReservations).ToList();

            if(historyUserQuarterOrYearLocalList != null)
                //On affiche la liste par ordre de mois du premier au dernier
                historyLocalBase.HistoryUserQuarterOrYearLocalList = historyUserQuarterOrYearLocalList.OrderBy(h => h.Mois).ToList();            

            return historyLocalBase;
        }

        private void FillYearAverage(FilterHistoryDto filterHistoryDto, List<HistoryUserLocal> historyUserLocalList)
        {
            var historyUserAvgLocalList = _dataLayer.GetNumberReservationsSpecificYearForAverageV2(filterHistoryDto);

            //Récupère la liste des utilisateurs qui ont réservés sur le mois courant
            foreach (var oneHistoryUser in historyUserLocalList)
            {
                List<HistoryUserAvgLocal> getAllReservationsByUser;

                if (IsSsoEnv.IsSso)
                {
                    getAllReservationsByUser = historyUserAvgLocalList.Where(avg => avg.ProprietaireId == oneHistoryUser.ProprietaireId).ToList();
                }
                else
                {
                    getAllReservationsByUser = historyUserAvgLocalList.Where(avg => avg.UserId == oneHistoryUser.UserId).ToList();
                }
                        
                double avgReservationsPerMonth = (double)(getAllReservationsByUser.Sum(avg => avg.NbReservationsMois)) / 12;
                oneHistoryUser.MoyenneAnnee = Math.Round(avgReservationsPerMonth, 1, MidpointRounding.AwayFromZero);
            }
        }

        private HistoryLocal GetAllUserMonthGeneric(FilterHistoryDto filterHistoryDto = null)
        {
            var historyUserMonthLocalList = _dataLayer.GetNumberReservationsSpecificMonthV2(filterHistoryDto);

            return FillHistoryLocal(filterHistoryDto, historyUserMonthLocalList);
        }

        private void GetTotalReservationsByUser(List<HistoryUserLocal> historyUserLocalList, List<HistoryUserQuarterOrYearLocal> historyUserQuarterOrYearList)
        {
            foreach (var oneHistoryUser in historyUserLocalList)
            {
                if (IsSsoEnv.IsSso)
                {
                    oneHistoryUser.NbTotalReservations = historyUserQuarterOrYearList.Where(h => h.ProprietaireId == oneHistoryUser.ProprietaireId).Sum(h => h.NbReservations);
                }
                else
                {
                    oneHistoryUser.NbTotalReservations = historyUserQuarterOrYearList.Where(h => h.UserId == oneHistoryUser.UserId).Sum(h => h.NbReservations);
                }
            }
        }

        private void AddMonthsForQuarterOrYear(FilterHistoryDto filterHistoryDto, List<HistoryUserLocal> historyUserLocalList, List<HistoryUserQuarterOrYearLocal> historyUserQuarterOrYearList)
        {
            int iStart = filterHistoryDto.Trimestre == 0 ? 1 : QuarterUtil.GetStartingMonthFromQuarter(filterHistoryDto.Trimestre);
            int iCondition = filterHistoryDto.Trimestre == 0 ? 12 : QuarterUtil.GetStartingMonthFromQuarter(filterHistoryDto.Trimestre + 1) - 1;

            foreach (var oneHistoryUser in historyUserLocalList)
            {
                for (int i = iStart; i <= iCondition; i++)
                {
                    int currentMonth = i;

                    if (IsSsoEnv.IsSso)
                    {
                        int findMonth = historyUserQuarterOrYearList.Where(p => p.Mois == currentMonth && p.ProprietaireId == oneHistoryUser.ProprietaireId).Select(p => p.Mois).FirstOrDefault();

                        if (currentMonth != findMonth)
                        {
                            historyUserQuarterOrYearList.Add(new HistoryUserQuarterOrYearLocal { ProprietaireId = oneHistoryUser.ProprietaireId, NbReservations = 0, Mois = currentMonth });
                        }
                    }
                    else
                    {
                        int findMonth = historyUserQuarterOrYearList.Where(p => p.Mois == currentMonth && p.UserId == oneHistoryUser.UserId).Select(p => p.Mois).FirstOrDefault();

                        if (currentMonth != findMonth)
                        {
                            historyUserQuarterOrYearList.Add(new HistoryUserQuarterOrYearLocal { UserId = oneHistoryUser.UserId, NbReservations = 0, Mois = currentMonth });
                        }
                    }
                }
            }
        }

        private HistoryLocal GetAllUserQuarterOrYearGeneric(FilterHistoryDto filterHistoryDto)
        {
            List<HistoryUserQuarterOrYearLocal> historyUserQuarterOrYearList = new List<HistoryUserQuarterOrYearLocal>();
            List<HistoryUserLocal> historyUserLocalList = new List<HistoryUserLocal>();

            if (filterHistoryDto != null && (filterHistoryDto.Trimestre > 0 || filterHistoryDto.Annee > 0))
            {
                //Récupère les réservations sur un trimestre avec une année selectionnée ou simplement une année
                historyUserQuarterOrYearList = _dataLayer.GetNumberReservationsSpecificTrimesterWithYearOrYearV2(filterHistoryDto);
                historyUserLocalList = _dataLayer.GetUsersWhoReservedSpecificYearV2(filterHistoryDto);

                GetTotalReservationsByUser(historyUserLocalList, historyUserQuarterOrYearList);
                AddMonthsForQuarterOrYear(filterHistoryDto, historyUserLocalList, historyUserQuarterOrYearList);
            }
            else
            {
                throw new ArgumentNullException(nameof(filterHistoryDto));
            }

            return FillHistoryLocal(filterHistoryDto, historyUserLocalList, historyUserQuarterOrYearList);
        }
        #endregion

        #region OnPost Filters
        /// <summary>
        /// Récupère les statistiques des réservations par utilisateur /1 mois/trimestre/année donné
        /// Fait également la moyenne des réservations /1 an par utilisateur
        /// </summary>
        /// <param name="filterHistoryDto"></param>
        public HistoryLocal GetAllFilter(FilterHistoryDto filterHistoryDto)
        {
            HistoryLocal historyLocalBase = new HistoryLocal();

            if(filterHistoryDto != null)
            {
                //Mois actuel ou mois selectionné dans le filtre
                if (filterHistoryDto.Mois > 0 && filterHistoryDto.Annee > 0)
                {
                    historyLocalBase = GetAllUserMonthGeneric(filterHistoryDto);
                }
                else if (filterHistoryDto.Trimestre > 0 || filterHistoryDto.Annee > 0)
                {
                    historyLocalBase = GetAllUserQuarterOrYearGeneric(filterHistoryDto);
                }
                else
                {
                    throw new NotFoundException(nameof(filterHistoryDto));
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(filterHistoryDto));
            }

            return historyLocalBase;
        }
        #endregion

        #region OnGet
        
        /// <summary>
        /// Récupère les statistiques des réservations par utilisateur le mois actuel
        /// Fait également la moyenne des réservations /1 an par utilisateur
        /// </summary>
        public HistoryLocal GetAll()
        {
            return GetAllUserMonthGeneric();
        }

        public HistoryFilterLocal GetYears()
        {
            HistoryFilterLocal historyFilterLocal = new HistoryFilterLocal();
            historyFilterLocal.Annee = _dataLayer.ExtractYears();

            if (historyFilterLocal.Annee.Count == 0)
            {
                //Aucune réservation n'est enregistré en BDD
                throw new ArgumentNullException(nameof(historyFilterLocal));
            }

            return historyFilterLocal;
        }
        #endregion
    }
}
