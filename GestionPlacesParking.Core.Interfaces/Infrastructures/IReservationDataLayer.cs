using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using GestionPlacesParking.Core.Models.Locals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Interfaces.Infrastructures
{
    public interface IReservationDataLayer
    {
        List<Reservation> GetAllReserved(DateOnly date);
        int AddOne(Reservation reservation);
    }
}
