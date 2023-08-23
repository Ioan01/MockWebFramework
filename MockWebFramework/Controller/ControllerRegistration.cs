using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MockWebFramework.Controller.Attributes.Endpoint;
using MockWebFramework.Http;
using MockWebFramework.Http.HttpExceptions;
using MockWebFramework.Service;


namespace MockWebFramework.Controller
{
    internal class ControllerRegistration
    {
        private readonly ServiceContainer _serviceContainer;

        // might be more efficient to do some sort of tree traversal through the path to do route matching
        //
        private List<Endpoint> endpoints = new();


        public ControllerRegistration(Type type, string controllerName, ServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
            ControllerName = controllerName;

            var dependencies = _serviceContainer.ResolveDependencies(type);
            Controller = Activator.CreateInstance(type,dependencies) ?? throw new Exception("Could not instantiate controller");

            MapRoutes(type);
        }

        private void MapRoutes(Type type)
        {
            foreach (var methodInfo in type.GetMethods())
            {
                if (methodInfo.IsPublic)
                {
                    var routeAttribute = methodInfo.GetCustomAttributes(false)
                        .FirstOrDefault(attribute => attribute is HttpRouteAttribute) as HttpRouteAttribute;

                    if (routeAttribute != null)
                    {
                        var endpoint = new Endpoint(methodInfo,routeAttribute);
                        endpoints.Add(endpoint);
                    }
                    
                }

            }
        }


        public string ControllerName { get; }

        public object Controller { get; }

        public object? Handle(HttpRequest request)
        {
            MatchCollection? matches = null;

            // try to find the requested endpoint
            var endpoint = endpoints.FirstOrDefault(endpoint =>
            {
                if (endpoint.Method == request.Method)
                {
                    matches = endpoint.PathMatcher.Matches(request.EndpointRoute);
                    if (matches.Count > 0)
                        return true;
                }

                return false;
            });

            // if not found try to find all endpoints with different methods to throw 
            // Method not allowed with the right allow headers
            if (endpoint == null)
            {
                var sameNameEndpoints = endpoints.Where(e => e.PathMatcher.IsMatch(request.EndpointRoute));
                if (sameNameEndpoints.Count() == 0)
                    throw new NotFoundException();
                // create allow header with all allowed methods for the endpoint
                throw new MethodNotAllowedException(null, new[]
                {
                    new Header("Allow", sameNameEndpoints.Aggregate("", (s, endpoint1) =>
                    {
                        if (String.IsNullOrEmpty(s))
                            return s + endpoint1.Method;
                        return s + $", {endpoint1.Method}";
                    }))
                });

            }

            var val =  endpoint.Invoke(Controller,request,matches);

            return val;
        }
    }
}
