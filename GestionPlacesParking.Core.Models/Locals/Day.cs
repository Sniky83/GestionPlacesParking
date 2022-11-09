using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Models.Locals
{
    public class Day
    {
        public Dictionary<string, DateOnly> DaysOfTheWeek { get; set; }
        public bool IsNextWeek { get; set; }
    }
}
