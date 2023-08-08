using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Networking
{
    internal class HttpRequest
    {
        private readonly int _maxMethodSize = 10;
        private readonly int _maxRouteSize = 2048;
        private readonly int _maxHeaderSize = 1024;


        private string Verb;

        private string Route;

        private Dictionary<string,string> Headers = new();

        private Memory<byte> content;

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

            while (buffer.Span[index] != ' ')
            {
                route.Append((char)buffer.Span[index++]);
            }

            Route = route.ToString();



            while (buffer.Span[index] != 0xd && buffer.Span[index+1] != 0xa)
            {
                index++;
            }

            index+=2;
        }

        private void ExtractHeaders(Memory<byte> buffer,ref int index)
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
                    InterpretHeader(buffer.Slice(headerStart, headerEnd-headerStart-1));
                    headerStart = headerEnd+1;
                    if (buffer.Span[headerStart] == 0xd)
                        if (buffer.Span[headerStart+1] == 0xa)
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

            Headers.Add(Encoding.UTF8.GetString(headerString.Span.Slice(0,headerNameIndex)),
                Encoding.UTF8.GetString(headerString.Span.Slice(headerNameIndex+2)));

        }

        public HttpRequest(Memory<byte> buffer)
        {

            int index = 0;

            ExtractRoute(buffer,ref index);


            ExtractHeaders(buffer.Slice(index),ref index);

            ExtractContent(buffer.Slice(index));

        }

        private void ExtractContent(Memory<byte> slice)
        {
            
            content = new byte[slice.Length];
            slice.CopyTo(content);

            Console.WriteLine(GetContentAsString());
        }

        public string GetContentAsString()
        {
            return Encoding.UTF8.GetString(content.Span);
        }
    }
}
