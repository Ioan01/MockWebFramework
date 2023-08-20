using MockWebFramework.Networking.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.HttpExceptions
{
    internal class InternalServerErrorException : HttpException
    {
        public InternalServerErrorException(string? message = null, IEnumerable<Header>? headers = null, Exception? innerException = null) : base(500, message,headers,innerException)
        {
        }
    }
}
