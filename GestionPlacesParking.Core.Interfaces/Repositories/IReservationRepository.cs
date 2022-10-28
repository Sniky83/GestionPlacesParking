using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        List<Reservation> GetAllReserved(DateOnly firstDayOfTheWeek);
        int MakeReservation(Reservation reservation);
    }
}
