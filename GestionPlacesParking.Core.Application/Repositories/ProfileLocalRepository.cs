using GestionPlacesParking.Core.Application.Utils;
using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals.Profile;

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
            for (int i = 0; i < 11; i++)
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
                oneProfileUserMonths.MoisString = DisplayNameUtil.GetMonthDisplayNameByMonth(oneProfileUserMonths.Mois);

                bool isReservations = profileAllUserMonthsList.Where(p => p.Mois == oneProfileUserMonths.Mois).Any();

                if (isReservations)
                {
                    oneProfileUserMonths.MoyenneMois = Queryable.Average(profileAllUserMonthsList
                        .Where(h => h.Mois == oneProfileUserMonths.Mois)
                        .Select(h => h.NbReservations)
                        .AsQueryable()
                    );
                }
            }

            int moisStartMoyenne = profileUserMonthsList.Where(p => p.MoyenneMois > 0).Select(p => p.Mois).FirstOrDefault();
            moisStartMoyenne = 5;

            ProfileLocal profileLocal = new ProfileLocal();

            profileLocal.ProfileUserMonthsListLocal = profileUserMonthsList;
            profileLocal.TotalReservations = Queryable.Sum(profileUserMonthsList.Select(p => p.NbReservations).AsQueryable());
            //Moyenne des réservations sur l'année à partir du mois ou les réservations ont commencés
            profileLocal.MaMoyenneAnnee = Queryable.Average(profileUserMonthsList.Where(p => p.Mois >= moisStartMoyenne).Select(p => p.NbReservations).AsQueryable());
            profileLocal.MoyenneAnnee = Queryable.Average(profileUserMonthsList.Where(p => p.Mois >= moisStartMoyenne).Select(p => p.MoyenneMois).AsQueryable());

            return profileLocal;
        }
    }
}
