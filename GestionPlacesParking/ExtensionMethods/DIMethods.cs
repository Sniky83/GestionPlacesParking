using GestionPlacesParking.Core.Application.Repositories;
using GestionPlacesParking.Core.Infrastructure.DataLayers;
using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;

namespace SelfieAWookieAPI.ExtensionMethods
{
    public static class DIMethods
    {
        #region Public Methods
        /// <summary>
        /// Prepare l'injection de dépendance personnalisée.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddInjections(this IServiceCollection Services)
        {
            // Injections de dépendances
            Services.AddScoped<IUserDataLayer, SqlServerUserDataLayer>();
            Services.AddScoped<IUserRepository, UserRepository>();

            Services.AddScoped<IParkingSlotDataLayer, SqlServerParkingSlotDataLayer>();
            Services.AddScoped<IParkingSlotRepository, ParkingSlotRepository>();

            Services.AddScoped<IReservationDataLayer, SqlServerReservationDataLayer>();
            Services.AddScoped<IReservationRepository, ReservationRepository>();

            //Pas de dataLayer car en Local
            Services.AddScoped<IDayLocalRepository, DayLocalRepository>();

            //Utilise le dataLayer de Réservation
            Services.AddScoped<IHistoryLocalRepository, HistoryLocalRepository>();
            Services.AddScoped<IProfileLocalRepository, ProfileLocalRepository>();

            return Services;
        }
        #endregion
    }
}
