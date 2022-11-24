namespace GestionPlacesParking.Core.Global.EnvironmentVariables
{
    internal class ConfigurationEnvVariable
    {
        private static string GetEnvironmentVariableByTarget(string EnvironmentVariableName, EnvironmentVariableTarget environmentVariableTarget)
        {
            string connectionString = Environment.GetEnvironmentVariable(EnvironmentVariableName, environmentVariableTarget);

            return connectionString;
        }

        public static string GetEnvironmentVariable(string EnvironmentVariableName)
        {
            string environmentString = GetEnvironmentVariableByTarget(EnvironmentVariableName, EnvironmentVariableTarget.User);

            if (environmentString == null)
            {
                environmentString = GetEnvironmentVariableByTarget(EnvironmentVariableName, EnvironmentVariableTarget.Process);

                if (environmentString == null)
                {
                    environmentString = GetEnvironmentVariableByTarget(EnvironmentVariableName, EnvironmentVariableTarget.Machine);
                }
            }

            return environmentString;
        }
    }
}
