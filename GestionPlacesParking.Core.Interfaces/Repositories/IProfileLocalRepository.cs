using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals.Profile;

namespace GestionPlacesParking.Core.Interfaces.Repositories
{
    public interface IProfileLocalRepository
    {
        ProfileLocal GetAll(GetProfileDto getProfileDto);
    }
}
