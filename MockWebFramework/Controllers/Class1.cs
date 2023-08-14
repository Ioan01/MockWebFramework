using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Controller;
using MockWebFramework.Controller.Attributes;

namespace MockWebFramework.Controllers
{
    [ControllerPrefix("/")]
    internal class Class1
    {
        [HttpGet("/a/id")]
        public void RouteHandler1([FromRoute]int id)
        {

        }
    }
}
