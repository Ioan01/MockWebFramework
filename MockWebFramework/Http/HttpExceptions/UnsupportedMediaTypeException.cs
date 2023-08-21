using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Http.HttpExceptions
{
    internal class UnsupportedMediaTypeException : HttpException
    {
        public UnsupportedMediaTypeException(string? message = null, IEnumerable<Header>? headers = null, Exception? innerException = null) : base(HttpStatusCode.UnsupportedMediaType, message, headers, innerException)
        {
        }
    }
}
