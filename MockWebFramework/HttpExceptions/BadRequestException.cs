namespace MockWebFramework.HttpExceptions
{
    internal class BadRequestException : HttpException
    {
        public BadRequestException(string? message = null) : base(400, "Bad Request",message)
        {
        }
    }
}
