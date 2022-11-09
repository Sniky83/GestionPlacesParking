using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Models.DTOs
{
    public class DeleteOneReservationDto
    {
        public int ReservationId { get; set; }
        public int? UserId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
