using GestionPlacesParking.Core.Infrastructure.Databases;

namespace GestionPlacesParking.Core.Infrastructure.DataLayers
{
    /// <summary>
    /// Classe parent pour tous les dataLayers qui vont avoir besoin du context
    /// Cette classe est abstract pour ne pas qu'on puisse l'instancier
    /// </summary>
    public abstract class BaseSqlServerDataLayer
    {
        #region Fields
        private readonly ParkingDbContext _context;
        #endregion

        #region Constructors
        public BaseSqlServerDataLayer(ParkingDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Properties
        protected ParkingDbContext Context { get => _context; }
        #endregion
    }
}
