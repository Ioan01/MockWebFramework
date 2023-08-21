using MockWebFramework.Http;

namespace MockWebFramework.Http.HttpExceptions
{
    internal class NotFoundException : HttpException
    {
        public NotFoundException(string? message = null, IEnumerable<Header>? headers = null, Exception? innerException = null) : base(HttpStatusCode.NotFound, message, headers, innerException)
        {

        }
    }
}
