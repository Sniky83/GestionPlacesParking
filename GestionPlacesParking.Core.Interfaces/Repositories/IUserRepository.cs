using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface pour implémenter le code métier
    /// </summary>
    public interface IUserRepository
    {
        User LogIn(UserDto user);
    }
}
