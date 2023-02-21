using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals.Profile;
using static GestionPlacesParking.Core.Models.Locals.History.HistoryFilterLocal;

namespace GestionPlacesParking.Core.Application.Repositories
{
    public class ProfileLocalRepository : IProfileLocalRepository
    {
        private readonly IReservationDataLayer _dataLayer;

        public ProfileLocalRepository(IReservationDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public ProfileLocal GetAll(GetProfileDto getProfileDto)
        {
            List<ProfileUserMonthsLocal> profileUserMonthsList = _dataLayer.GetNumberReservationsThisYear(getProfileDto);
            List<ProfileAllUserMonthsLocal> profileAllUserMonthsList = _dataLayer.GetNumberReservationsByMonths();

            //On parcours tous les mois de l'année
            for (int i = 0; i < 12; i++)
            {
                int currentMonth = (i + 1);

                int findMonth = profileUserMonthsList.Where(p => p.Mois == currentMonth).Select(p => p.Mois).FirstOrDefault();

                if (currentMonth != findMonth)
                {
                    profileUserMonthsList.Add(new ProfileUserMonthsLocal { NbReservations = 0, Mois = currentMonth });
                }
            }

            profileUserMonthsList = profileUserMonthsList.OrderBy(p => p.Mois).ToList();

            foreach (var oneProfileUserMonths in profileUserMonthsList)
            {
                bool isReservations = profileAllUserMonthsList.Where(p => p.Mois == oneProfileUserMonths.Mois).Any();

                if (isReservations)
                {
                    double avgReservationsPerMonth = profileAllUserMonthsList.Where(h => h.Mois == oneProfileUserMonths.Mois).Sum(h => h.NbReservations) / profileAllUserMonthsList.Where(h => h.Mois == oneProfileUserMonths.Mois).Count();

                    oneProfileUserMonths.MoyenneMois = Math.Round(avgReservationsPerMonth, 1, MidpointRounding.AwayFromZero);
                }
            }

            int moisStartMoyenne = _dataLayer.GetFirstMonthReserved(DateTime.Now.Year);

            ProfileLocal profileLocal = new ProfileLocal();

            profileLocal.ProfileUserMonthsListLocal = profileUserMonthsList;
            profileLocal.TotalReservations = profileUserMonthsList.Sum(p => p.NbReservations);
            //Moyenne des réservations sur l'année à partir du mois où les réservations ont commencés
            profileLocal.MaMoyenneAnnee = Math.Round((double)(profileUserMonthsList.Where(p => p.Mois >= moisStartMoyenne).Sum(p => p.NbReservations)) / 12, 1, MidpointRounding.AwayFromZero);
            profileLocal.MoyenneAnnee = Math.Round((double)(profileUserMonthsList.Where(p => p.Mois >= moisStartMoyenne).Sum(p => p.MoyenneMois)) / 12, 1, MidpointRounding.AwayFromZero);

            return profileLocal;
        }
    }
}
