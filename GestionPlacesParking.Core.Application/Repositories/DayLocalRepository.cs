using GestionPlacesParking.Core.Application.Exceptions;
using GestionPlacesParking.Core.Global.BusinessLogics;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.Locals;

namespace GestionPlacesParking.Core.Application.Repositories
{
    public class DayLocalRepository : IDayLocalRepository
    {
        public DayLocal GetDaysWithDate()
        {
            int daysToDeduce = 0;
            int dayOfWeek = (int)DateTime.Today.DayOfWeek;

            switch (dayOfWeek)
            {
                //Lundi
                case 1:
                    daysToDeduce = 0;
                    break;
                //Mardi
                case 2:
                    daysToDeduce = 1;
                    break;
                //Mercredi
                case 3:
                    daysToDeduce = 2;
                    break;
                //Jeudi
                case 4:
                    daysToDeduce = 3;
                    break;
                //Vendredi
                case 5:
                    daysToDeduce = 4;
                    break;
                //Samedi
                case 6:
                    daysToDeduce = 5;
                    break;
                //Dimanche
                case 7:
                    daysToDeduce = 6;
                    break;
                default:
                    //Signifie que le code est outdated
                    throw new NotFoundException(nameof(dayOfWeek));
            }

            int numberDaysInWeek = 7;
            int maxMonths = 12;
            bool isNextMonth = false;
            bool isPreviousMonth = false;
            int dayToday = DateTime.Today.Day;
            int lastMonth = DateTime.Now.Month - 1;

            int firstDayOfTheWeek = (dayToday - daysToDeduce);

            //firstDayOfTheWeek doit au moins être = à 1
            if (dayToday <= daysToDeduce)
            {
                //Si le mois actuel == 1 (Janvier)
                if (DateTime.Now.Month == 1)
                    //Mois précédent == 12 (Décembre)
                    lastMonth = 12;

                int lastMonthDays = DateTime.DaysInMonth(DateTime.Now.Year, lastMonth);

                firstDayOfTheWeek = ((dayToday + lastMonthDays) - daysToDeduce);
                isPreviousMonth = true;
            }

            //Règle métier: Si on est vendredi >= à 11h00
            if (ReservationBusinessLogic.IsEndReservationsCurrentWeek())
            {
                //On passe au lundi d'après
                firstDayOfTheWeek += 7;
            }

            int currentYear = DateTime.Now.Year;
            int currentMonth = (isPreviousMonth == true ? lastMonth : DateTime.Now.Month);
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            string dateFormat = "dd/MM/yyyy";

            List<string> dateForeachDaysList = new List<string>();

            for (int i = 0; i < numberDaysInWeek; i++)
            {
                if (firstDayOfTheWeek > daysInMonth)
                {
                    if (!isNextMonth)
                    {
                        if ((currentMonth + 1) > maxMonths)
                        {
                            currentYear += 1;
                        }

                        firstDayOfTheWeek = 1;
                        currentMonth = (currentMonth == 12 ? 1 : currentMonth + 1);
                        isNextMonth = true;
                    }
                }

                string date = new DateOnly(currentYear, currentMonth, firstDayOfTheWeek).ToString(dateFormat);

                firstDayOfTheWeek += 1;

                dateForeachDaysList.Add(date);
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