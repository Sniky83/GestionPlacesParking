namespace GestionPlacesParking.Core.Global.BusinessLogics
{
    public static class EveryFridayBusinessLogic
    {
        public static bool IsEndReservationsCurrentWeek()
        {
            DateTime currentTime = DateTime.Now;

            //Règle métier: Si on est vendredi >= à 11h00
            if ((int)currentTime.DayOfWeek >= 5 && currentTime.Hour >= 11 && currentTime.Minute >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
