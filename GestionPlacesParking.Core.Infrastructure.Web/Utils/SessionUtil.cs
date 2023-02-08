using GestionPlacesParking.Core.Global.Consts;
using GestionPlacesParking.Core.Global.EnvironmentVariables.Envs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Infrastructure.Web.Utils
{
    internal static class SessionUtil
    {
        public static dynamic GetUserId(HttpContext context)
        {
            dynamic? userId = null;

            if (IsSsoEnv.IsSso)
            {
                userId = context.Session.GetString(SessionConst.UserId);
            }
            else
            {
                userId = context.Session.GetInt32(SessionConst.UserId);
            }

            return userId;
        }
    }
}
