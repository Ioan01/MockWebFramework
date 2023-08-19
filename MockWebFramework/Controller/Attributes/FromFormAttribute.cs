using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Controller.Attributes
{
    /// <summary>
    /// Specifies that the parameter will be extracted from a application/x-www-form-urlencoded form
    /// </summary>
    internal class FromFormAttribute : ParameterFromAttribute
    {
        public FromFormAttribute() : base(ParameterSource.FromForm)
        {
        }
    }
}
