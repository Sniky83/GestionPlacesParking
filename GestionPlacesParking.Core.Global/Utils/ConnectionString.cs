namespace GestionPlacesParking.Web.UI.Utils
{
    public static class ConnectionString
    {
        private static string GetConnectionStringEnv(EnvironmentVariableTarget environmentVariableTarget)
        {
            string connectionString = Environment.GetEnvironmentVariable("ParkingContextConnectionString", environmentVariableTarget);

            return connectionString;
        }

        public static string GetConnectionString()
        {
            string connectionString = GetConnectionStringEnv(EnvironmentVariableTarget.User);

            if (connectionString == null)
            {
                connectionString = GetConnectionStringEnv(EnvironmentVariableTarget.Process);

                if (connectionString == null)
                {
                    connectionString = GetConnectionStringEnv(EnvironmentVariableTarget.Machine);
                }
            }

            return connectionString;
        }
    }
}
