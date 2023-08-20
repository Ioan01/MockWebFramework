using MockWebFramework.Networking.HttpRequest;

namespace MockWebFramework.HttpExceptions
{
    internal class BadRequestException : HttpException
    {
        public BadRequestException(string? message = null,IEnumerable<Header>?headers = null,Exception? innerException = null) : base(400, message,headers, innerException)
        {
        }
    }
}
