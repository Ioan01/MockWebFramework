using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Controller.Attributes
{
    /// <summary>
    /// Used to mark controllers functions as http request handlers, specifying the HTTP method, and optionally
    /// a route name (if left blank, the function name will be used)
    /// </summary>
    internal class HttpRouteAttribute : Attribute
    {
        public HttpRouteAttribute(string method,string? route)
        {
            Route = route;
            Method = method;
        }

        public HttpRouteAttribute(string method)
        {
            Method = method;
        }

        public string? Route { get;}

        public string Method { get; }
    }
}
