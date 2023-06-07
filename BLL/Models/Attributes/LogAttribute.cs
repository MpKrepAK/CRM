using System.Reflection;
using BLL.Models.Attributes.Interfaces;
using BLL.Models.Services.Logging.Implementations;
using BLL.Models.Services.Logging.Interfaces;

namespace BLL.Models.Attributes;

public class LogAttribute : Attribute, IAttributeMessages
{
    public ILogger _Logger { get; set; }

    public LogAttribute()
    {
        _Logger = new FileLogger();
    }
    public async Task OnEnter(MethodInfo methodInfo)
    {
        await _Logger.Log($"Метод {methodInfo.Name} был вызван");
    }

    public async Task OnExit(MethodInfo methodInfo)
    {
        await _Logger.Log($"Метод {methodInfo.Name} завершен успешно");
    }

    public async Task OnThrows(MethodInfo methodInfo, Exception exception)
    {
        await _Logger.LogError($"Метод {methodInfo.Name} завершен с ошибкой {exception.Message}");
    }
}