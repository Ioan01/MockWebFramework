using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Networking.HttpRequest.Body
{
    public static class ContentTypes
    {
        public const string ContentType = "Content-Type";
        public const string PlainText = "text/plain";
        public const string Json = "application/json";
        public const string Xml = "application/xml";
        public const string Html = "text/html";
        public const string File = "application/octet-stream";
        public const string MultipartForm = "multipart/form-data";
        public const string Form = "application/x-www-form-urlencoded";


    }

    internal abstract class HttpBody
    {
        protected Memory<byte> contentBytes;

        public  int ContentLength => contentBytes.Length;
        public byte[] Bytes => contentBytes.ToArray();

        public abstract string? GetParameter(string name);

        public static HttpBody ParseBody(string? contentType,Memory<byte> content)
        {
            HttpBody body = null;


            switch (contentType)
            {
                case ContentTypes.PlainText:
                    body = new TextBody(content);
                    break;
                case ContentTypes.Json:
                    body = new JsonBody(content);
                    break;
                case ContentTypes.Form:
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
