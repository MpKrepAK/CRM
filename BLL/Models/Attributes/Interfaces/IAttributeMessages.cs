using System.Reflection;

namespace BLL.Models.Attributes.Interfaces;

public interface IAttributeMessages
{
    Task OnEnter(MethodInfo methodInfo);
    Task OnExit(MethodInfo methodInfo);
    Task OnThrows(MethodInfo methodInfo, Exception exception);
}