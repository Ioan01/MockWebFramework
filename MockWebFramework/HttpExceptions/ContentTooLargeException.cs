namespace MockWebFramework.HttpExceptions
{
    internal class ContentTooLargeException : HttpException
    {
        public ContentTooLargeException(string? message = null) : base(413, "Content Too Large",message)
        {
        }
    }
}
