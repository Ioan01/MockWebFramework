using System.Reflection;
using MockWebFramework.Logging;
using MockWebFramework.Service.Attributes;

namespace MockWebFramework.Service;

internal class ServiceHost
{
    private readonly ServiceContainer _container = new ServiceContainer();
    public ServiceContainer ServiceContainer => _container;


    public void AddSingleton<T>()
    {
        _container.RegisterService(typeof(T), ServiceLifespan.Singleton);
    }

    public void AddPrototype<T>()
    {
        _container.RegisterService(typeof(T), ServiceLifespan.Prototype);
    }

    public void RegisterServices(string @namespace = "Services")
    {
        var services = Assembly.GetExecutingAssembly().GetTypes().Where(t =>
            t.Namespace != null && !t.Namespace.StartsWith("System") && t.Namespace.Contains(@namespace));
        foreach (var service in services)
        {
            var lifepan = service.GetCustomAttributes()
                .FirstOrDefault(attr => attr is ServiceLifespanAttribute) as ServiceLifespanAttribute;
            if (lifepan != null)
                _container.RegisterService(service, lifepan.Lifespan);
            else _container.RegisterService(service, ServiceLifespan.Singleton);
        }
    }

    
}