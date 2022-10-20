using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Models.Locals
{
    public class Day
    {
        public Dictionary<string, DateOnly> DaysOfTheWeek { get; set; }

        public Day()
        {
            ExtractDaysWithNumbers();
        }

        private void ExtractDaysWithNumbers()
        {
            int daysToDeduce = 0;

            switch (DateTime.Today.DayOfWeek.ToString().ToLower())
            {
                case "monday":
                    daysToDeduce = 0;
                    break;
                case "tuesday":
                    daysToDeduce = 1;
                    break;
                case "wednesday":
                    daysToDeduce = 2;
                    break;
                case "thursday":
                    daysToDeduce = 3;
                    break;
                case "friday":
                    daysToDeduce = 4;
                    break;
                case "saturday":
                    daysToDeduce = 5;
                    break;
                case "sunday":
                    daysToDeduce = 6;
                    break;
            }

            int numberDaysInWeek = 7;
            int maxMonths = 12;
            int firstDayNumber = 1;
            bool isNextMonth = false;

            int firstDayOfTheWeek = (DateTime.Today.Day - daysToDeduce);
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);

            List<DateOnly> dateForeachDaysList = new List<DateOnly>();

            for (int i = 0; i < numberDaysInWeek; i++)
            {
                if (firstDayOfTheWeek > daysInMonth)
                {
                    if (!isNextMonth)
                    {
                        if((currentMonth + 1) > maxMonths)
                        {
                            currentYear += 1;
                        }

                        currentMonth += 1;
                        isNextMonth = true;
                    }

                    DateOnly date = new DateOnly(currentYear, currentMonth, firstDayNumber);

                    dateForeachDaysList.Add(date);
                    firstDayNumber += 1;
                }
                else
                {
                    DateOnly date = new DateOnly(currentYear, currentMonth, firstDayOfTheWeek);

                    dateForeachDaysList.Add(date);
                    firstDayOfTheWeek += 1;
                }
            }

            DaysOfTheWeek = new Dictionary<string, DateOnly>()
            {
                { "Lundi", dateForeachDaysList[0] },
                { "Mardi", dateForeachDaysList[1] },
                { "Mercredi", dateForeachDaysList[2] },
                { "Jeudi", dateForeachDaysList[3] },
                { "Vendredi", dateForeachDaysList[4] },
                { "Samedi", dateForeachDaysList[5] },
                { "Dimanche", dateForeachDaysList[6] }
            };
        }
    }
}
