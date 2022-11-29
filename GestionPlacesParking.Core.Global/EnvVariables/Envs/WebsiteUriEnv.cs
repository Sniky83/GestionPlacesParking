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
                throw new ArgumentNullException(nameof(websiteUriEnv));
            }

            return websiteUriEnv;
        }
    }
}
