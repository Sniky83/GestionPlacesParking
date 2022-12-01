using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Models.Locals;

namespace GestionPlacesParking.Core.Infrastructure.DataLayers
{
    public class DayLocalDataLayer : IDayLocalDataLayer
    {
        DayLocal IDayLocalDataLayer.ExtractDaysWithDate(List<string> dateForeachDaysList)
        {
            DayLocal dayLocal = new DayLocal();

            dayLocal.DaysOfTheWeek = new Dictionary<string, string>()
            {
                { "Lundi", dateForeachDaysList[0] },
                { "Mardi", dateForeachDaysList[1] },
                { "Mercredi", dateForeachDaysList[2] },
                { "Jeudi", dateForeachDaysList[3] },
                { "Vendredi", dateForeachDaysList[4] },
                { "Samedi", dateForeachDaysList[5] },
                { "Dimanche", dateForeachDaysList[6] }
            };

            return dayLocal;
        }
    }
}
