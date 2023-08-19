namespace MockWebFramework.HttpExceptions
{
    internal class NotFoundException : HttpException
    {
        public NotFoundException(string? message = null) : base(404,"Not found",message)
        {

        }
    }
}
