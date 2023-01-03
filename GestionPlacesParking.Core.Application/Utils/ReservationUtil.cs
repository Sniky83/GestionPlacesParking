using GestionPlacesParking.Core.Global.EnvironmentVariables.Envs;
using KeycloakCore.Keycloak;
using Newtonsoft.Json.Linq;

namespace GestionPlacesParking.Core.Application.Utils
{
    internal static class ReservationUtil
    {
        //On ne return rien car la List de réservation est modifiée depuis le param
        public static void FillAllReservingName(dynamic userList)
        {
            if (IsSsoEnv.IsSso)
            {
                var webManager = new WebManager();
                var userInfojson = webManager.GetAllUserInfo();

                dynamic jsonObject = JArray.Parse(userInfojson);

                //On prends les données keycloak pour remplir le nom prenom de l'user
                foreach (var oneUser in userList)
                {
                    foreach (var jsonObj in jsonObject)
                    {
                        string proprietaireId = jsonObj.id;

                        if (oneUser.ProprietaireId == proprietaireId)
                        {
                            string fullName = jsonObj.firstName + " " + jsonObj.lastName;
                            oneUser.ReservingName = fullName;
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (var oneUser in userList)
                {
                    oneUser.ReservingName = oneUser.User.FirstName + " " + oneUser.User.LastName;
                }
            }
        }
    }
}
