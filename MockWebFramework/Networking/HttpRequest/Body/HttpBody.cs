using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Networking.HttpRequest.Body
{
    public static class ContentTypes
    {
        public const string PlainText = "text/plain";
        public const string Json = "application/json";
        public const string Xml = "application/xml";
        public const string Html = "text/html";
        public const string File = "application/octet-stream";
        public const string MultipartForm = "multipart/form-data";
        public const string Form = "application/x-www-form-urlencoded";


    }

    internal class HttpBody
    {
        protected Memory<byte> contentBytes;

        private string? _contentString = null;

        public string Content
        {
            get
            {
                if (_contentString == null)
                    _contentString = Encoding.UTF8.GetString(contentBytes.Span);
                return _contentString;
            }
        }


        public static HttpBody ParseBody(string contentType)
        {
            switch (contentType)
            {
                case ContentTypes.PlainText:
                    return new TextBody();
                case ContentTypes.Json:
                    return new JsonBody();
                default:
                    return new HttpBody();
            }
        }
    }
}
