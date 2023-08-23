using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Service.Attributes
{
    internal class PrototypeAttribute : ServiceLifespanAttribute
    {
        public PrototypeAttribute() : base(ServiceLifespan.Prototype)
        {
        }
    }
}
