﻿namespace GestionPlacesParking.Core.Models.Locals.History
{
    public class HistoryUserLocalV1
    {
        public string ProprietaireId { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; }
        public int Mois { get; set; } = 0;
        public string ReservingName { get; set; } = string.Empty;
        //Vide si plusieurs mois
        //Utile si un seul mois
        public int NbReservationsMois { get; set; }
        public double MoyenneAnnee { get; set; }
        public int TotalReservations { get; set; }
    }
}