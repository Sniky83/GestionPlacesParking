using GestionPlacesParking.Core.Application.Exceptions;

namespace GestionPlacesParking.Core.Application.Utils
{
    internal static class DisplayNameUtil
    {
        public static string GetMonthDisplayNameByMonth(int month)
        {
            string displayMonth = string.Empty;

            switch (month)
            {
                case 1:
                    displayMonth = "Janvier";
                    break;
                case 2:
                    displayMonth = "Février";
                    break;
                case 3:
                    displayMonth = "Mars";
                    break;
                case 4:
                    displayMonth = "Avril";
                    break;
                case 5:
                    displayMonth = "Mai";
                    break;
                case 6:
                    displayMonth = "Juin";
                    break;
                case 7:
                    displayMonth = "Juillet";
                    break;
                case 8:
                    displayMonth = "Août";
                    break;
                case 9:
                    displayMonth = "Septembre";
                    break;
                case 10:
                    displayMonth = "Octobre";
                    break;
                case 11:
                    displayMonth = "Novembre";
                    break;
                case 12:
                    displayMonth = "Décembre";
                    break;
                default:
                    throw new NotFoundException(nameof(displayMonth));
            }

            return displayMonth;
        }

        public static string GetQuarterDisplayNameByMonth(int month)
        {
            string displayQuarter = string.Empty;

            switch (month)
            {
                case <= 3:
                    displayQuarter = "Premier";
                    break;
                case <= 6:
                    displayQuarter = "Second";
                    break;
                case <= 9:
                    displayQuarter = "Troisième";
                    break;
                case <= 12:
                    displayQuarter = "Quatrième";
                    break;
                default:
                    throw new NotFoundException(nameof(displayQuarter));
            }

            return displayQuarter;
        }

        public static string GetQuarterDisplayNameByQuarter(int quarter)
        {
            string displayQuarter = string.Empty;

            switch (quarter)
            {
                case 1:
                    displayQuarter = "Premier";
                    break;
                case 2:
                    displayQuarter = "Second";
                    break;
                case 3:
                    displayQuarter = "Troisième";
                    break;
                case 4:
                    displayQuarter = "Quatrième";
                    break;
                default:
                    throw new NotFoundException(nameof(displayQuarter));
            }

            return displayQuarter;
        }
    }
}
