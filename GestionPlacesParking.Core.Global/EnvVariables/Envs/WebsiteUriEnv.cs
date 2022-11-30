namespace GestionPlacesParking.Core.Global.EnvironmentVariables.Envs
{
    public static class WebsiteUriEnv
    {
        public static readonly string WebsiteUri = GetWebsiteUriEnv();
        private static string GetWebsiteUriEnv()
        {
            string websiteUriEnv = ConfigurationEnvVariable.GetEnvironmentVariable("WebsiteUri");

            if (websiteUriEnv == null)
            {
                bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

                if (isDevelopment)
                {
                    websiteUriEnv = "https://localhost:7041";
                }

                throw new ArgumentNullException(nameof(websiteUriEnv));
            }

            return websiteUriEnv;
        }
    }
}
