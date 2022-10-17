using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Models
{
    /// <summary>
    /// Place de parking
    /// </summary>
    public class ParkingSlot
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
    }
}
