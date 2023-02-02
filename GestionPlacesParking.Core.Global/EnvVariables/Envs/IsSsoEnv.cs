namespace GestionPlacesParking.Core.Global.EnvironmentVariables.Envs
{
    public static class IsSsoEnv
    {
        public static readonly bool IsSso = GetIsSsoEnv();
        private static bool GetIsSsoEnv()
        {
            return ConfigurationEnvVariable.GetEnvironmentVariable("IsSso") == "1" ? true : false;
        }
    }
}
