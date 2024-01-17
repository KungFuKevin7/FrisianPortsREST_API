namespace FrisianPortsREST_API.Error_Logger
{
    public class ErrorLogger : ILoggerService
    {

        private readonly ILogger<ILoggerService> _logger;

        public ErrorLogger(ILogger<ILoggerService> logger) 
        {
            _logger = logger;
        }

        /// <summary>
        /// Log Information to Console
        /// </summary>
        /// <param name="logMessage">message to log</param>
        public void Log(string logMessage)
        {
            _logger.LogInformation(FormatLog(logMessage));
        }

        /// <summary>
        /// Log Warning to Console
        /// </summary>
        /// <param name="warning">warning to log</param>
        public void LogWarning(string warning) 
        {
            _logger.LogWarning(FormatLog(warning));
        }

        /// <summary>
        /// Log error to console
        /// </summary>
        /// <param name="e">Received exception</param>
        public void LogError(Exception e) 
        {
            _logger.LogError(FormatLog(e));
        }

        /// <summary>
        /// Formats the log, to display time, message and location of the error
        /// </summary>
        /// <param name="exception">Received exception</param>
        /// <returns>
        /// formatted string displaying time, exception and location
        /// </returns>
        public string FormatLog(Exception exception)
        {
            return @$"[{DateTime.Now}]: {exception.Message}
                      {exception.StackTrace}";
        }

        /// <summary>
        /// Formats an error that is not of type Exception
        /// </summary>
        /// <param name="error">Error message to log</param>
        /// <returns>String with time and error message</returns>
        public string FormatLog(string error) 
        {
            return @$"[{DateTime.Now}]: {error}";
        }

    }
}
