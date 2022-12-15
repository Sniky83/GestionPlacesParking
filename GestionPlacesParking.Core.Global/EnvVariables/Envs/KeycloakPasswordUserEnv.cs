using GestionPlacesParking.Core.Global.EnvVariables.Envs;

namespace GestionPlacesParking.Core.Global.EnvironmentVariables.Envs
{
    public static class KeycloakPasswordUserEnv
    {
        public static readonly string KeycloakPasswordUser = GetKeycloakPasswordUserEnv();
        private static string GetKeycloakPasswordUserEnv()
        {
            string KeycloakPasswordUserEnv = ConfigurationEnvVariable.GetEnvironmentVariable("KeycloakPasswordUser");

            if (KeycloakPasswordUserEnv == null)
            {
                bool isDevelopment = IsDevelopmentEnv.IsDevelopment;

                if (isDevelopment)
                {
                    return KeycloakPasswordUserEnv = "pyLcVV$tAFRaNv3J";
                }

                throw new ArgumentNullException(nameof(KeycloakPasswordUserEnv));
            }

            return KeycloakPasswordUserEnv;
        }
    }
}
