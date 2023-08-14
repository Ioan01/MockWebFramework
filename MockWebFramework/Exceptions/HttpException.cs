using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Exceptions
{
    internal class HttpException : Exception
    {
        public string Name { get; }

        public int Code { get; }

        public HttpException(int code,string name)
        {
            Name = name;
            Code = code;
        }

    }
}
