using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Networking.HttpRequest
{
    internal class HttpQuery
    {
        public Dictionary<string, string> Parameters { get; } = new();

        public HttpQuery()
        {
            
        }
    }
}
