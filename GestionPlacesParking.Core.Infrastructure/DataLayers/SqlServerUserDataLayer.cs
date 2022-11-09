using GestionPlacesParking.Core.Infrastructure.Databases;
using GestionPlacesParking.Core.Infrastructure.Web.Cipher;
using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Infrastructure.DataLayers
{
    public class SqlServerUserDataLayer : BaseSqlServerDataLayer, IUserDataLayer
    {
        public SqlServerUserDataLayer(ParkingDbContext context) : base(context) { }

        public User GetOne(AuthenticationUserDto user)
        {
            return Context?.Users.Where(item => item.Email == user.Email && item.Password == Sha256Cipher.Hash(user.Password)).First();
        }
    }
}
