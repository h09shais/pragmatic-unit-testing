using Shopping.Core.Models;

namespace Shopping.Core.Services
{
    public interface ILoggingService
    {
        void Log(LogLevel logLevel, string message);
    }
}
