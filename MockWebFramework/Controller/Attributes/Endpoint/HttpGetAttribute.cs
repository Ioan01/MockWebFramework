using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Controller.Attributes.Endpoint
{
    internal class HttpGetAttribute : HttpRouteAttribute
    {
        public HttpGetAttribute(string? route = null,string? produces = null) : base("GET", route,produces)
        {
        }

      
    }
}
