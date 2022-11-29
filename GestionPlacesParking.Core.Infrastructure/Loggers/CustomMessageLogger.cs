using Microsoft.Extensions.Logging;

namespace SelfieAWookie.Core.Selfies.Infrastructures.Loggers
{
    public class CustomMessageLogger : ILogger
    {
        #region Public Methods
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.Trace;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string customMessage = $"[{DateTime.Now}]: #{logLevel}# {formatter(state, exception)}";
            Console.WriteLine(customMessage);

            //On log que les erreurs et les warnings dans le fichier
            if (logLevel >= LogLevel.Warning)
            {
                using StreamWriter w = File.AppendText("log.txt");

                w.WriteLine(customMessage);
            }
        }
        #endregion
    }
}
