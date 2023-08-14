using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Controller.Attributes;
using MockWebFramework.Logging;
using MockWebFramework.Networking.HttpRequest;

namespace MockWebFramework.Controller
{
    internal class ControllerRegistration
    {
        private Dictionary<(string,string), Endpoint> endpoints = new();


        public ControllerRegistration(Type type,string controllerName)
        {
            ControllerName = controllerName;

            Controller = Activator.CreateInstance(type) ?? throw new Exception("Could not instantiate controller");

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
                        var endpoint = new Endpoint(methodInfo);
                    }
                    


                }






            }
        }


        public string ControllerName { get; }

        public object Controller { get; }

        public object Handle(HttpRequest request)
        {
            return null;
        }
    }
}
