using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.HttpExceptions;
using MockWebFramework.Networking.HttpRequest.Body;

namespace MockWebFramework.Networking.HttpRequest
{
    internal class HttpRequest
    {
        private readonly int _maxMethodSize = 10;
        private readonly int _maxRouteSize = 2048;
        private readonly int _maxHeaderSize = 1024;


        public string Method { get; private set; }

        public string[] RouteList { get; private set; }

        public string EndpointRoute { get; private set; }

        public Dictionary<string, Header?> Headers { get; } = new();

        public HttpBody Body { get; private set; }

        public HttpQuery Query { get; private set; }


        private void ExtractRoute(Memory<byte> buffer, ref int index)
        {
            StringBuilder verb = new StringBuilder(_maxMethodSize);
            StringBuilder route = new StringBuilder(_maxRouteSize);

            while (buffer.Span[index] != ' ')
            {
                verb.Append((char)buffer.Span[index++]);
            }

            index++;

            Method = verb.ToString();

            int routeNumber = 1;

            while (buffer.Span[index] != ' ')
            {
                route.Append((char)buffer.Span[index++]);
                if (buffer.Span[index] == '/')
                    routeNumber++;
            }

            var routeStr = route.ToString();

            RouteList = routeStr.Split('/').Skip(1).ToArray();

            if (RouteList.Last() != String.Empty)
            {
                var queryIndex = RouteList.Last().IndexOf('?');
                //RouteList
                if (queryIndex != -1)
                {
                    Query = new HttpQuery(RouteList.Last().Substring(queryIndex));
                    EndpointRoute = routeStr.Substring(0, routeStr.IndexOf('?'));

                }
                else EndpointRoute = routeStr.Substring(RouteList[0].Length+1);
            }
            else EndpointRoute = String.Empty;

            while (buffer.Span[index] != 0xd && buffer.Span[index + 1] != 0xa)
            {
                index++;
            }

            index += 2;
        }

        private void ExtractHeaders(Memory<byte> buffer, ref int index)
        {
            bool doubleEnding = false;

            int headerStart = 0;
            int headerEnd = 0;


            while (!doubleEnding)
            {
                while (buffer.Span[headerEnd] != 0xd)
                {
                    headerEnd++;
                }

                headerEnd++;

                if (buffer.Span[headerEnd] == 0xa)
                {
                    InterpretHeader(buffer.Slice(headerStart, headerEnd - headerStart - 1));
                    headerStart = headerEnd + 1;
                    if (buffer.Span[headerStart] == 0xd)
                        if (buffer.Span[headerStart + 1] == 0xa)
                            doubleEnding = true;
                }
                else break;


            }

            index += headerStart;
        }

        private void InterpretHeader(Memory<byte> headerString)
        {
            int headerNameIndex = 0;
            while (headerString.Span[headerNameIndex] != ':')
            {
                headerNameIndex++;
            }

            var headerName = Encoding.UTF8.GetString(headerString.Span.Slice(0, headerNameIndex));

            var headerValue = Encoding.UTF8.GetString(headerString.Span.Slice(headerNameIndex + 2));

            Headers.Add(headerName,new Header(headerName,headerValue));

        }

        public HttpRequest(Memory<byte> buffer)
        {

            int index = 0;

            ExtractRoute(buffer, ref index);


            ExtractHeaders(buffer.Slice(index), ref index);

            ExtractContent(buffer.Slice(index));

        }

        

        private void ExtractContent(Memory<byte> slice)
        {
            // slice should start with 0xd 0xa followed by 0 if body is empty, or values if body is not empty
            if (slice.Length > 3)
            {
                if (slice.Span[3] == 0)
                {
                    return;
                }
            }

            Header? contentType = null;

            if (Headers.ContainsKey("Content-Type"))
                contentType = Headers["Content-Type"];

            // exception handler for incorrect bodies
            try
            {
                Body = HttpBody.ParseBody(contentType?.Values[0], slice.Slice(0, slice.Span.IndexOf((byte)0)));

            }
            catch (Exception e)
            {
                throw new BadRequestException();
            }
        }

    }
}
