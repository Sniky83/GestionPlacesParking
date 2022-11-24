using KeycloakCore.Keycloak;
using Newtonsoft.Json;

namespace Keycloak.Core.SSO.Keycloak
{
    internal class Settings
    {
        public string KeycloakUrl = string.Empty;
        public string Realm = string.Empty;
        public string ClientId = string.Empty;
        public string ClientSecret = string.Empty;
        public string BaseUri = string.Empty;
        public string CallbackUrl = string.Empty;

        public Settings()
        {
            //ReadJson();
        }

        private static void ReadJson()
        {
            string path = new DirectoryInfo(Environment.CurrentDirectory).Parent.FullName + "\\Keycloak.Core.SSO\\Keycloak\\" + "keycloak.Development.json";
            StreamReader r = new StreamReader(path);
            string json = r.ReadToEnd();
            JsonConvert.DeserializeObject<Settings>(json);
        }

        public readonly SingleSignOnSettings SsoSettings = new SingleSignOnSettings()
        {
            KeycloakUrl = "https://keycloak.indus.aix.apsdigit.lan/auth",
            Realm = "gestionPDP",
            ClientId = "gestionPDP",
            ClientSecret = "c4e66325-eb1e-4e14-9b9a-ab630e8468d0",
            BaseUri = "https://localhost:7041/",
            CallbackUrl = "https://localhost:7041/Callback"
        };
    }
}
