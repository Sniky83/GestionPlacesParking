using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Application.Repositories
{
    public class DayRepository : IDayRepository
    {
        public Day ExtractDaysWithDate()
        {
            int daysToDeduce = 0;

            switch ((int)DateTime.Today.DayOfWeek)
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
            }

            int numberDaysInWeek = 7;
            int maxMonths = 12;
            int firstDayNumber = 1;
            bool isNextMonth = false;

            DateTime currentTime = DateTime.Now;
            int firstDayOfTheWeek = (DateTime.Today.Day - daysToDeduce);

            Day day = new Day();

            //Règle métier: Si on est au moins vendredi à 11h00
            if ((int)currentTime.DayOfWeek >= 5 && currentTime.Hour >= 11 && currentTime.Minute >= 0)
            {
                //On passe au lundi d'après
                firstDayOfTheWeek += 7;
                day.IsNextWeek = true;
            }
                
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
                        if ((currentMonth + 1) > maxMonths)
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

            day.DaysOfTheWeek = new Dictionary<string, DateOnly>()
            {
                { "Lundi", dateForeachDaysList[0] },
                { "Mardi", dateForeachDaysList[1] },
                { "Mercredi", dateForeachDaysList[2] },
                { "Jeudi", dateForeachDaysList[3] },
                { "Vendredi", dateForeachDaysList[4] },
                { "Samedi", dateForeachDaysList[5] },
                { "Dimanche", dateForeachDaysList[6] }
            };

            return day;
        }
    }
}
