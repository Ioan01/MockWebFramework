using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Networking.Http.Response
{
    internal class OK : HttpResponse
    {
        public OK(object? body = null) : base(200, "OK", body)
        {

        }
    }
}
