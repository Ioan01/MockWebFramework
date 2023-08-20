using MockWebFramework.Networking.HttpRequest;

namespace MockWebFramework.HttpExceptions
{
    internal class ContentTooLargeException : HttpException
    {
        public ContentTooLargeException(string? message = null, IEnumerable<Header>? headers = null, Exception? innerException = null) : base(413,message,headers,innerException)
        {
        }
    }
}
