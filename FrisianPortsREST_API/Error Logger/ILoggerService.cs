namespace FrisianPortsREST_API.Error_Logger
{
    /// <summary>
    /// Logging Implementation
    /// </summary>
    public interface ILoggerService
    {
        void Log(string logMessage);
        void LogError(Exception exception);
        void LogWarning(string logMessage);
    }
}
