namespace MockWebFramework.HttpExceptions
{
    internal class HttpException : Exception
    {
        public string Name { get; }

        public int Code { get; }

        public string? Message { get; }

        public HttpException(int code,string name, string? message = null)
        {
            Name = name;
            Code = code;
            Message = message;
        }

    }
}
