using GestionPlacesParking.Core.Models.Locals;

namespace GestionPlacesParking.Core.Interfaces.Repositories
{
    public interface IDayLocalRepository
    {
        DayLocal GetDaysWithDate();
    }
}
