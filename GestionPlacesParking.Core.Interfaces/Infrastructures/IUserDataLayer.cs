using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Interfaces.Infrastructures
{
    /// <summary>
    /// Interface pour implémenter les requêtes
    /// </summary>
    public interface IUserDataLayer
    {
        User GetOne(AuthenticationUserDto user);
    }
}
