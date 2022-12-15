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

            for (int i = 0; i < 11; i++)
            {
                int currentMonth = i + 1;
                if (currentMonth != profileUserMonthsList[i].Mois)
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

            ProfileLocal profileLocal = new ProfileLocal();

            profileLocal.ProfileUserMonthsListLocal = profileUserMonthsList;
            profileLocal.TotalReservations = Queryable.Sum(profileUserMonthsList.Select(p => p.NbReservations).AsQueryable());
            profileLocal.MaMoyenneAnnee = Queryable.Average(profileUserMonthsList.Select(p => p.NbReservations).AsQueryable());
            profileLocal.MoyenneAnnee = Queryable.Average(profileUserMonthsList.Select(p => p.MoyenneMois).AsQueryable());

            return profileLocal;
        }
    }
}
