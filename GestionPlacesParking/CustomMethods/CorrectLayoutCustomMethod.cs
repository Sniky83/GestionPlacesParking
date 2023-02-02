using GestionPlacesParking.Core.Global.Consts;

namespace GestionPlacesParking.Web.UI.CustomMethods
{
    public static class CorrectLayoutCustomMethod
    {
        public static string GetCorrectLayout(HttpContext httpContext)
        {
            int? isAdmin = httpContext.Session.GetInt32(SessionConst.IsAdmin);

            if (isAdmin == 1)
            {
                return "_AdminLayout";
            }
            else
            {
                return "_Layout";
            }
        }
    }
}
