using GestionPlacesParking.Core.Global.BusinessLogics;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.Locals;

namespace GestionPlacesParking.Core.Application.Repositories
{
    public class DayLocalRepository : IDayLocalRepository
    {
        public DayLocal GetDaysWithDate()
        {
            DateTime currentDate = DateTime.Now;

            DayOfWeek currentDayOfWeek = currentDate.DayOfWeek;

            int daysToSkip = (int)(currentDayOfWeek - 1);
            if (daysToSkip < 0)
            {
                daysToSkip += 7;
            }

            DateTime firstMondayOfWeek = currentDate.AddDays(-daysToSkip);

            List<DateTime> weekDates = new List<DateTime>();

            for (int i = 0; i < 7; i++)
            {
                weekDates.Add(firstMondayOfWeek.AddDays(i));
            }

            //Règle métier: Si on est vendredi >= à 11h00
            if (ReservationBusinessLogic.IsEndReservationsCurrentWeek())
            {
                //On passe au lundi d'après
                firstMondayOfWeek.AddDays(7);
            }

            List<string> dateForeachDaysList = new List<string>();

            foreach (DateTime weekDate in weekDates)
            {
                dateForeachDaysList.Add(weekDate.ToShortDateString());
            }

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