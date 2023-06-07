namespace BLL.Models.Services.Logging.Interfaces;

public interface ILogger
{
    Task Log(string message);
    Task LogError(string message);
}