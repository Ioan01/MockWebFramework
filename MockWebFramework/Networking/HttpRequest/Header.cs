using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Networking.HttpRequest
{
    internal class Header
    {
        public string Name { get; }

        public string[] Values { get; } 

        public Header(string headerName, string headerValue)
        {
            Name = headerName;

            Values = headerValue.Split(',');
        }
    }
}
