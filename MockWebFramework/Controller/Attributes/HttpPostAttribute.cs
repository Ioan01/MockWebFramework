using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Controller.Attributes
{
    internal class HttpPostAttribute : HttpRouteAttribute
    {
        public HttpPostAttribute(string? route) : base("POST", route)
        {
        }

        public HttpPostAttribute() : base("POST")
        {
            
        }
    }
}
