using GestionPlacesParking.Core.Models.Locals;

namespace GestionPlacesParking.Core.Interfaces.Infrastructures
{
    public interface IDayLocalDataLayer
    {
        DayLocal ExtractDaysWithDate(List<string> dateForeachDaysList);
    }
}
