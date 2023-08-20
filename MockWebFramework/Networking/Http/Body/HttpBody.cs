using MockWebFramework.Networking.HttpRequest;
using MockWebFramework.Networking.HttpRequest.Body;
using System.Net.Mime;

namespace MockWebFramework.Networking.Http.Body
{
    public static class ContentTypes
    {
        public const string ContentType = "Content-Type";
        public const string TextPlain = "text/plain";
        public const string Json = "application/json";
        public const string Xml = "application/xml";
        public const string Html = "text/html";
        public const string File = "application/octet-stream";
        public const string MultipartForm = "multipart/form-data";
        public const string FormData = "application/x-www-form-urlencoded";

        private const string contentType = "Content-Type";

        public static readonly Dictionary<string, Header> ContentTypeHeaders = new Dictionary<string, Header>
        {
            { Json, new Header(contentType, Json) },
            { Xml, new Header(contentType, Xml) },
            { FormData, new Header(contentType, FormData) },
            { TextPlain, new Header(contentType, TextPlain) },
            { MultipartForm, new Header(contentType, MultipartForm) },
            { File, new Header(contentType,File) },
            { Html, new Header(contentType,Html) }
        };

        public static Header? GetContentTypeHeader(string contentType)
        {
            return ContentTypeHeaders.TryGetValue(contentType, out var header) ? header : null;
        }
    }

    internal abstract class HttpBody
    {
        protected Memory<byte> contentBytes;

        public  int ContentLength => contentBytes.Length;
        public byte[] Bytes => contentBytes.ToArray();

        public abstract Header? ContentTypeHeader { get; }

        public abstract string? GetParameter(string name);

        public static HttpBody ParseBody(string? contentType,Memory<byte> content)
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
