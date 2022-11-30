using GestionPlacesParking.Core.Global.EnvironmentVariables.Envs;
using KeycloakCore.Keycloak;
using Newtonsoft.Json;

namespace Keycloak.Core.SSO.Keycloak
{
    internal class Settings
    {
        public class JsonSettings
        {
            [JsonProperty("auth-server-url")]
            public string KeycloakUrl { get; set; } = string.Empty;
            [JsonProperty("realm")]
            public string Realm { get; set; } = string.Empty;
            [JsonProperty("resource")]
            public string ClientId { get; set; } = string.Empty;
            [JsonProperty("credentials")]
            public JsonCredentials Credentials { get; set; }
        }

        public class JsonCredentials
        {
            [JsonProperty("secret")]
            public string ClientSecret { get; set; } = string.Empty;
        }
        public JsonSettings JSettings { get; set; }
        public SingleSignOnSettings SsoSettings { get; set; }

        public Settings()
        {
            ReadJson();
            FillSsoSettings();
        }

        private void ReadJson()
        {
            bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            string keycloakFile = string.Empty;

            if (isDevelopment)
            {
                keycloakFile = "keycloak.Development.json";
            }
            else
            {
                keycloakFile = "keycloak.Production.json";
            }

            string keycloakPath = Path.Combine(Directory.GetCurrentDirectory(), "Settings", keycloakFile);

            StreamReader r = new StreamReader(keycloakPath);
            string json = r.ReadToEnd();
            JSettings = JsonConvert.DeserializeObject<JsonSettings>(json);
        }

        private void FillSsoSettings()
        {
            string uriPath = "/API/LoginCallback";
            string baseUri = WebsiteUriEnv.WebsiteUri;
            string callbackUrl = WebsiteUriEnv.WebsiteUri + uriPath;

            SsoSettings = new SingleSignOnSettings()
            {
                KeycloakUrl = JSettings.KeycloakUrl.Remove(JSettings.KeycloakUrl.Length - 1),
                Realm = JSettings.Realm,
                ClientId = JSettings.ClientId,
                ClientSecret = JSettings.Credentials.ClientSecret,
                BaseUri = baseUri,
                CallbackUrl = callbackUrl
            };
        }
    }
}
