using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Controller.Attributes
{
    /// <summary>
    /// Specifies that the parameter will be taken from the route of the http request (/books/Sans-Famille/details)
    /// </summary>
    internal class FromRouteAttribute : ParameterFromAttribute
    {
        public FromRouteAttribute() : base(ParameterSource.FromRoute)
        {
        }
    }
}
