using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Exceptions
{
    internal class BadRequestException : HttpException
    {
        public BadRequestException(string? message = null) : base(400, "Bad Request",message)
        {
        }
    }
}
