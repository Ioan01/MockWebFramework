using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Http
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

        // file content types

        public const string Jpeg = "image/jpeg";
        public const string Png = "image/png";
        public const string Pdf = "application/pdf";
        public const string Javascript = "application/javascript";
        public const string Css = "text/css";
        public const string Zip = "application/zip";
        public const string Mp3 = "audio/mpeg";
        public const string Mp4 = "video/mp4";

        private const string contentType = "Content-Type";

        public static readonly Dictionary<string, Header> ContentTypeHeaders = new Dictionary<string, Header>
        {
            { Json, new Header(contentType, Json) },
            { Xml, new Header(contentType, Xml) },
            { FormData, new Header(contentType, FormData) },
            { TextPlain, new Header(contentType, TextPlain) },
            { MultipartForm, new Header(contentType, MultipartForm) },
            { File, new Header(contentType,File) },
            { Html, new Header(contentType,Html) },

            { Jpeg, new Header(ContentType, Jpeg) },
        { Png, new Header(ContentType, Png) },
        { Pdf, new Header(ContentType, Pdf) },
        { Javascript, new Header(ContentType, Javascript) },
        { Css, new Header(ContentType, Css) },
        { Zip, new Header(ContentType, Zip) },
        { Mp3, new Header(ContentType, Mp3) },
        { Mp4, new Header(ContentType, Mp4) },
        };

        public static Header? GetContentTypeHeader(string contentType)
        {
            return ContentTypeHeaders.TryGetValue(contentType, out var header) ? header : null;
        }
    }
}
