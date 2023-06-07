using System.Reflection;
using BLL.Models.Attributes;

namespace BLL.Models.Services.Proxy;

public class ProxyReposytoryCreator : DispatchProxy
{
    private IAttributeSerializeble _target;
    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        var logAttribute = targetMethod.GetCustomAttribute<LogAttribute>();
        if (logAttribute == null)
            return targetMethod.Invoke(_target, args);
        
        logAttribute.OnEnter(targetMethod);
        object result;
        try
        {
            result = targetMethod.Invoke(_target, args);
        }
        catch (Exception e)
        {
            logAttribute.OnThrows(targetMethod, e);
            throw;
        }
        logAttribute.OnExit(targetMethod);
        return result;
    }
    public static T Create<T>(IAttributeSerializeble logging)
    {
        object proxy = Create<T, ProxyObjectCreater>();
        ((ProxyObjectCreater)proxy).SetParameters(logging);
        return (T)proxy;
    }

    public void SetParameters(IAttributeSerializeble attribute)
    {
        _target = attribute;
    }
}