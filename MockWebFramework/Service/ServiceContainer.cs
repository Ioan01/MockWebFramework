﻿using MockWebFramework.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Service
{
    internal class ServiceContainer
    {
        private readonly List<ServiceRegistration> _services = new();

        public void RegisterService(Type serviceType, ServiceLifespan lifespan)
        {
            var registration = new ServiceRegistration(serviceType, lifespan, this);

            var constructors = serviceType.GetConstructors();

            if (constructors.Length != 0)
                registration.ServiceDependencies = constructors[0].GetParameters().Select(p => p.ParameterType).ToArray();

            _services.Add(registration);

            VerifyCircularDependencies(serviceType, registration.ServiceDependencies);
        }

        // service does not use services that require that same service
        private void VerifyCircularDependencies(Type serviceType, Type[] registrationServiceDependencies)
        {
            if (registrationServiceDependencies.Length == 0) return;

            if (serviceType.GetConstructors().Length == 0) return;

            if (serviceType.GetConstructors()[0].GetParameters().Length == 0) return;

            foreach (var dependency in registrationServiceDependencies)
                if (dependency.GetConstructors().Length > 0)
                    if (dependency.GetConstructors()[0].GetParameters().Length > 0)
                        if (dependency.GetConstructors()[0].GetParameters().Any(p => p.ParameterType == serviceType))
                            Ilogger.Instance.LogFatal(
                                $"Circular dependency detected {serviceType.Name} depends on {dependency.Name} which also depends on {serviceType.Name}");
        }

        public object? GetService(Type type)
        {
            var registration = _services.FirstOrDefault(s => s.ServiceType.IsAssignableFrom(type));
            if (registration == null) return null;

            return registration.Service;
        }

        public object[] ResolveDependencies(ServiceRegistration registration)
        {
            var dependencies = new object[registration.ServiceDependencies.Length];
            var index = 0;

            foreach (var registrationServiceDependency in registration.ServiceDependencies)
            {
                var dependency = GetService(registrationServiceDependency);
                if (dependency == null)
                    Ilogger.Instance.LogFatal(
                        $"Could not satisfy dependency: {registration.ServiceType.Name} uses {registrationServiceDependency.Name}.");

                dependencies[index++] = dependency;
            }

            return dependencies;
        }

        public object[] ResolveDependencies(Type type)
        {
            if (type.GetConstructors().Length == 0)
                return Array.Empty<object>();

            if (type.GetConstructors()[0].GetParameters().Length == 0)
                return Array.Empty<object>();


            var dependencies = new object[type.GetConstructors()[0].GetParameters().Length];
            var i = 0;

            foreach (var dependency in type.GetConstructors()[0].GetParameters())
            {
                var service = GetService(dependency.ParameterType);
                if (service == null)
                    Ilogger.Instance.LogFatal($"Could not satisfy dependency: {type.Name} uses {dependency.Name}.");

                dependencies[i++] = service;
            }

            return dependencies;
        }

    }
}
