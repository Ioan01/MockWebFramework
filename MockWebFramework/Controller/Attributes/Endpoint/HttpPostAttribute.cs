using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Controller.Attributes.Endpoint
{
    internal class HttpPostAttribute : HttpRouteAttribute
    {
        public HttpPostAttribute(string? route = null,string? contentType = null) : base("POST", route, contentType)
        {
        }

        public HttpPostAttribute() : base("POST")
        {

        }
    }
}
