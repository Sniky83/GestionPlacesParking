namespace GestionPlacesParking.Core.Global.EnvironmentVariables.Envs
{
    public static class ConnectionStringEnv
    {
        public static readonly string ConnectionString = GetConnectionStringEnv();
        private static string GetConnectionStringEnv()
        {
            string connectionStringEnv = ConfigurationEnvVariable.GetEnvironmentVariable("ParkingContextConnectionString");

            if (connectionStringEnv == null)
            {
                throw new ArgumentNullException(nameof(connectionStringEnv));
            }

            return connectionStringEnv;
        }
    }
}
