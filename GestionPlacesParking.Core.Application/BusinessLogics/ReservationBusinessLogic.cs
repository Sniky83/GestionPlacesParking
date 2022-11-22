namespace GestionPlacesParking.Core.Global.BusinessLogics
{
    public static class ReservationBusinessLogic
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

        public static bool IsReservationDateInRange(DateTime reservationDate)
        {
            bool isEndReservations = IsEndReservationsCurrentWeek();

            //Si on est pas vendredi >= à 11h00 (semaine en cours)
            if (!isEndReservations)
            {
                DateTime thisMonday = DateTime.Now.AddDays(DayOfWeek.Monday - DateTime.Now.DayOfWeek).Date;
                DateTime thisFriday = thisMonday.AddDays(4).Date;

                //On veut que la date soit comprise dans cette range sinon l'user cherche à réserver en avance
                if (reservationDate >= thisMonday && reservationDate <= thisFriday)
                {
                    return true;
                }
            }
            else
            {
                DateTime nextMonday = DateTime.Now.AddDays((DayOfWeek.Monday - DateTime.Now.DayOfWeek) + 7).Date;
                DateTime nextFriday = nextMonday.AddDays(4).Date;

                if (reservationDate >= nextMonday && reservationDate <= nextFriday)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
