namespace MockWebFramework.Service;

internal enum ServiceLifespan
{
    // one instance of the service at all times
    Singleton,

    // unique instance created in every service injection
    Prototype
}

internal class ServiceRegistration
{
    private readonly ServiceContainer _host;

    public ServiceRegistration(Type serviceType, ServiceLifespan lifespan, ServiceContainer host)
    {
        ServiceType = serviceType;
        _host = host;
        ServiceLifespan = lifespan;
    }

    public Type ServiceType { get;  }

    public Type[] ServiceDependencies { get; set; }

    public ServiceLifespan ServiceLifespan { get; }

    public string ServiceName { get; }


    private object? _serviceInstance;

    public object? Service
    {
        get
        {
            if (ServiceLifespan == ServiceLifespan.Prototype)
            {
                var dependencies = _host.ResolveDependencies(this);
                return Activator.CreateInstance(ServiceType, dependencies);
            }

            if (_serviceInstance == null)
            {
                var dependencies = _host.ResolveDependencies(this);
                _serviceInstance = Activator.CreateInstance(ServiceType, dependencies);
            }
            


            return _serviceInstance;
        }
    }
}