using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Controller
{
    internal class Endpoint
    {
        private MethodInfo method;



        public Endpoint(MethodInfo method)
        {
            this.method = method;

            foreach (var parameterInfo in this.method.GetParameters())
            {
                
            }
        }
    }
}
