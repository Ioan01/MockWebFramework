using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Http.Response
{
    internal class OK : HttpResponse
    {
        public OK(object? body = null) : base(HttpStatusCode.OK, "OK", body)
        {

        }
    }
}
