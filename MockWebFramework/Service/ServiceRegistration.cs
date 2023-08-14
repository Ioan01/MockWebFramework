namespace MockWebFramework.Service
{
    enum LifeSpan
    {
        Singleton,
        Transient
    }


    internal class ServiceRegistration
    {
        public Type ServiceType { get; set; }

        public string ServiceName { get; set; }

        public object Service { get; set; }


    }
}
