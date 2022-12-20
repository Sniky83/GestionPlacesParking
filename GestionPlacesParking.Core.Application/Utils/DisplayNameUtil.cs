using GestionPlacesParking.Core.Application.Exceptions;

namespace GestionPlacesParking.Core.Application.Utils
{
    internal static class DisplayNameUtil
    {
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
    }
}
