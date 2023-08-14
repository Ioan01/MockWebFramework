using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Controller.Attributes
{
    internal class HttpRouteAttribute : Attribute
    {
        public HttpRouteAttribute(string method,string? route)
        {
            Route = route;
            Method = method;
        }

        protected HttpRouteAttribute(string method)
        {
            Method = method;
        }

        public string? Route { get;}

        public string Method { get; }
    }
}
