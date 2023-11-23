namespace FrisianPortsREST_API.Error_Logger
{
    public class ErrorLogger : ILoggerService
    {

        private readonly ILogger<ILoggerService> _logger;

        public ErrorLogger(ILogger<ILoggerService> logger) 
        {
            _logger = logger;
        }

        public void Log(string logMessage)
        {
            _logger.LogInformation(FormatLog(logMessage));
        }

        public void LogWarning(string warning) 
        {
            _logger.LogWarning(FormatLog(warning));
        }

        public void LogError(Exception e) 
        {
            _logger.LogError(FormatLog(e));
        }

        public string FormatLog(Exception exception) 
        {
            return @$"[{DateTime.Now}]: {exception.Message}
                      {exception.StackTrace}";
        }

        public string FormatLog(string error) 
        {
            return @$"[{DateTime.Now}]: {error}";
        }

    }
}
