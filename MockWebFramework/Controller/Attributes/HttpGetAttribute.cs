using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Controller.Attributes
{
    internal class HttpGetAttribute : HttpRouteAttribute
    {
        public HttpGetAttribute(string? route) : base("GET", route)
        {
        }

        public HttpGetAttribute() : base("GET")
        {
            
        }
    }
}
