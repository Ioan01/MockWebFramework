using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Service.Attributes
{
    
    internal class ServiceLifespanAttribute : Attribute
    {
        public ServiceLifespan Lifespan { get; set; }

        public ServiceLifespanAttribute(ServiceLifespan lifespan)
        {
            Lifespan = lifespan;
        }
    }
}
