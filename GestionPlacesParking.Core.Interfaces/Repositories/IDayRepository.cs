using GestionPlacesParking.Core.Models.Locals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Interfaces.Repositories
{
    public interface IDayRepository
    {
        Day ExtractDaysWithDate();
    }
}
