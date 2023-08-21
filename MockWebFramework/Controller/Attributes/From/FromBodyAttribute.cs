using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Controller.Attributes.From
{
    /// <summary>
    /// Specifies that the parameter will be extracted from the body of the http request
    /// </summary>
    internal class FromBodyAttribute : ParameterFromAttribute
    {
        public FromBodyAttribute() : base(ParameterSource.FromBody)
        {
        }
    }
}
