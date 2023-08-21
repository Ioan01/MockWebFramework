using MockWebFramework.Http;

namespace MockWebFramework.Http.HttpExceptions
{
    internal class BadRequestException : HttpException
    {
        public BadRequestException(string? message = null, IEnumerable<Header>? headers = null, Exception? innerException = null) : base(HttpStatusCode.BadRequest, message, headers, innerException)
        {
        }
    }
}
