using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Controller.Attributes
{
    /// <summary>
    /// Base class specifying where the endpoint parameters will be extracted from
    /// </summary>
    internal abstract class ParameterFromAttribute : Attribute
    {
        public ParameterFromAttribute(ParameterSource parameterSource)
        {
            ParameterSource = parameterSource;
        }

        public ParameterSource ParameterSource { get;}
    }
}
