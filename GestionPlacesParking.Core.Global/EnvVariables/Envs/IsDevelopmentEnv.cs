namespace GestionPlacesParking.Core.Global.EnvVariables.Envs
{
    public static class IsDevelopmentEnv
    {
        public static readonly bool IsDevelopment = GetIsDevelopmentEnv();
        private static bool GetIsDevelopmentEnv()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        }
    }
}