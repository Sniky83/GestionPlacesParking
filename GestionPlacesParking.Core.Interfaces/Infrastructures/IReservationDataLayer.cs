using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Interfaces.Infrastructures
{
    public interface IReservationDataLayer
    {
        List<Reservation> GetAllReserved();
        int AddOne(Reservation reservation);
    }
}
