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
                    startingMonth = 4;
                    break;
                case 3:
                    startingMonth = 7;
                    break;
                case 4:
                    startingMonth = 10;
                    break;
                case 5:
                    startingMonth = 13;
                    break;
            }

            return startingMonth;
        }
    }
}
