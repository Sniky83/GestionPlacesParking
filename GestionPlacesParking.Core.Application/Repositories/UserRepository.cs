using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;

namespace GestionPlacesParking.Core.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserDataLayer _dataLayer;

        public UserRepository(IUserDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }
        public User LogIn(UserDto user)
        {
            var userData = _dataLayer.GetOne(user);

            if (userData == null)
            {
                throw new ArgumentNullException(nameof(userData));
            }

            return userData;
        }
    }
}