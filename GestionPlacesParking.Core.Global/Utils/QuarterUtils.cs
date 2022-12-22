namespace GestionPlacesParking.Core.Global.Utils
{
    public static class QuarterUtils
    {
        public static int GetStartingMonthFromQuarter(int quarter)
        {
            int startingMonth = 0;

            switch (quarter)
            {
                case 1:
                    startingMonth = 1;
                    break;
                case 2:
                    startingMonth = 3;
                    break;
                case 3:
                    startingMonth = 6;
                    break;
                case 4:
                    startingMonth = 9;
                    break;
            }

            return startingMonth;
        }
    }
}
