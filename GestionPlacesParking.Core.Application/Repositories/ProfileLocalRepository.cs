﻿using GestionPlacesParking.Core.Interfaces.Infrastructures;
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
                oneProfileUserMonths.MoisString = Enum.GetName(typeof(Mois), oneProfileUserMonths.Mois);

                bool isReservations = profileAllUserMonthsList.Where(p => p.Mois == oneProfileUserMonths.Mois).Any();

                if (isReservations)
                {
                    oneProfileUserMonths.MoyenneMois = Queryable.Average(profileAllUserMonthsList
                        .Where(h => h.Mois == oneProfileUserMonths.Mois)
                        .Select(h => h.NbReservations)
                        .AsQueryable()
                    );

                    oneProfileUserMonths.MoyenneMois = Math.Round(oneProfileUserMonths.MoyenneMois, 1);
                }
            }

            int moisStartMoyenne = _dataLayer.GetFirstMonthReserved(DateTime.Now.Year);

            ProfileLocal profileLocal = new ProfileLocal();

            profileLocal.ProfileUserMonthsListLocal = profileUserMonthsList;
            profileLocal.TotalReservations = Queryable.Sum(profileUserMonthsList.Select(p => p.NbReservations).AsQueryable());
            //Moyenne des réservations sur l'année à partir du mois où les réservations ont commencés
            profileLocal.MaMoyenneAnnee = Math.Round(Queryable.Average(profileUserMonthsList.Where(p => p.Mois >= moisStartMoyenne).Select(p => p.NbReservations).AsQueryable()), 1);
            profileLocal.MoyenneAnnee = Math.Round(Queryable.Average(profileUserMonthsList.Where(p => p.Mois >= moisStartMoyenne).Select(p => p.MoyenneMois).AsQueryable()), 1);

            return profileLocal;
        }
    }
}
