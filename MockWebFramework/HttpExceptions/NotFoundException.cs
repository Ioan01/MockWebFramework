using MockWebFramework.Networking.HttpRequest;

namespace MockWebFramework.HttpExceptions
{
    internal class NotFoundException : HttpException
    {
        public NotFoundException(string? message = null, IEnumerable<Header>? headers = null, Exception? innerException = null) : base(404, message, headers, innerException)
        {

        }
    }
}
