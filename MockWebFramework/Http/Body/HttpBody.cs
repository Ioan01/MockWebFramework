using System.Net.Mime;

namespace MockWebFramework.Http.Body
{
    

    internal abstract class HttpBody
    {
        protected Memory<byte> contentBytes;

        public int ContentLength => contentBytes.Length;
        public byte[] Bytes => contentBytes.ToArray();

        public abstract Header? ContentTypeHeader { get; }

        public abstract string? GetParameter(string name);

        public static HttpBody ParseBody(string? contentType, Memory<byte> content)
        {
            HttpBody body = null;


            switch (contentType)
            {
                case ContentTypes.TextPlain:
                    body = new TextBody(content);
                    break;
                case ContentTypes.Json:
                    body = new JsonBody(content);
                    break;
                case ContentTypes.FormData:
                    body = new FormBody(content);
                    break;
                case ContentTypes.MultipartForm:
                    body = new MultipartFormBody(content);
                    break;
                default:
                    body = new TextBody(content);
                    break;
            }



            return body;
        }

        public HttpBody(Memory<byte> contentBytes)
        {
            this.contentBytes = new Memory<byte>(new byte[contentBytes.Length]);
            contentBytes.CopyTo(this.contentBytes);
        }
    }
}
