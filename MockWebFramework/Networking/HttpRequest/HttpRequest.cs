using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Networking.HttpRequest.Body;

namespace MockWebFramework.Networking.HttpRequest
{
    internal class HttpRequest
    {
        private readonly int _maxMethodSize = 10;
        private readonly int _maxRouteSize = 2048;
        private readonly int _maxHeaderSize = 1024;


        public string Verb { get; private set; }

        public string[] Route { get; private set; }

        public Dictionary<string, Header> Headers { get; } = new();

        public HttpBody Body { get; private set; }


        private void ExtractRoute(Memory<byte> buffer, ref int index)
        {
            StringBuilder verb = new StringBuilder(_maxMethodSize);
            StringBuilder route = new StringBuilder(_maxRouteSize);

            while (buffer.Span[index] != ' ')
            {
                verb.Append((char)buffer.Span[index++]);
            }

            index++;

            Verb = verb.ToString();

            int routeNumber = 1;

            while (buffer.Span[index] != ' ')
            {
                route.Append((char)buffer.Span[index++]);
                if (buffer.Span[index] == '/')
                    routeNumber++;
            }

            Route = route.ToString().Split('/').Skip(1).ToArray();

            if (Route.Last() != String.Empty)
            {
                //Route
                if (Route.Last()[0] == '?')
                {
                    // query

                }
            }

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
            Console.WriteLine($"Header : {Encoding.UTF8.GetString(headerString.Span)}");

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
            var contentType = Headers["Content-Type"];



            if (contentType != null)

            contentBytes = new byte[slice.Length];
            slice.CopyTo(contentBytes);

            Console.WriteLine(Content);
        }

    }
}
