using MockWebFramework.Http;

namespace MockWebFramework.Http.HttpExceptions
{
    internal class ContentTooLargeException : HttpException
    {
        public ContentTooLargeException(string? message = null, IEnumerable<Header>? headers = null, Exception? innerException = null) : base(HttpStatusCode.PayloadTooLarge, message, headers, innerException)
        {
        }
    }
}
