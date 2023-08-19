using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Controller;
using MockWebFramework.Controller.Attributes;

namespace MockWebFramework.Controllers
{
    [ControllerPrefix("/a")]
    internal class Class1
    {
        [HttpGet("/b/c")]
        public void RouteHandler2([FromQuery] int a,[FromBody]float b, [FromRoute]int c)
        {

        }

        [HttpGet("/a/id")]
        public void RouteHandler1([FromRoute]int id)
        {

        }
    }
}
