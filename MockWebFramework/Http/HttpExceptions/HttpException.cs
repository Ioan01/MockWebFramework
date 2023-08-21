using System.Net;
using MockWebFramework.Http;

namespace MockWebFramework.Http.HttpExceptions
{
    internal class HttpException : Exception
    {

        public string Name { get; }

        public HttpStatusCode Code { get; }

        public IEnumerable<Header>? Headers { get; }

        public HttpException(HttpStatusCode code, string? message = null, IEnumerable<Header>? headers = null,
            Exception? innerException = null) : base(message, innerException)
        {
            Name = HttpStatusNames.GetStatusCodeName(code);
            Headers = headers;
            Code = code;
        }

       

    }
}
