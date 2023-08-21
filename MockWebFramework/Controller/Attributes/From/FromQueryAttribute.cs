using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Controller.Attributes
{
    /// <summary>
    /// Specifies that the parameter will be extracted from the query (?param=value&...)
    /// </summary>
    internal class FromQueryAttribute : ParameterFromAttribute
    {
        public FromQueryAttribute() : base(ParameterSource.FromQuery)
        {
        }
    }
}
