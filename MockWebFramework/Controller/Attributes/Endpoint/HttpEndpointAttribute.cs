using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Controller.Attributes.Endpoint
{
    /// <summary>
    /// Used to mark controllers functions as http request handlers, specifying the HTTP method, and optionally
    /// a route name (if left blank, the function name will be used)
    /// </summary>
    internal class HttpRouteAttribute : Attribute
    {
        public HttpRouteAttribute(string method, string? route = null,string? produces = null)
        {
            Route = route;
            Produces = produces;
            Method = method;
        }

        

        public string? Route { get; }

        public string Method { get; }

        public string? Produces { get; }
    }
}
