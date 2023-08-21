using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Http.Body;

namespace MockWebFramework.Http.Response
{
    internal class HttpResponse
    {
        public HttpStatusCode StatusCode { get; }

        public string StatusName { get; }

        public List<Header> Headers { get; } = new List<Header>();

         
        public HttpBody? Body { get; }

        private static readonly byte[] newLine = { 0xd, 0xa };


        public HttpResponse(HttpStatusCode statusCode, string statusName, object? body = null)
        {
            StatusCode = statusCode;
            StatusName = statusName;

            if (body == null) return;

            if (body.GetType().IsPrimitive || body is string)
            {
                Body = new TextBody(Encoding.UTF8.GetBytes(body.ToString() ?? string.Empty));
                Headers.Add(new Header(ContentTypes.ContentType, ContentTypes.TextPlain));
            }

            else if (body is HttpBody httpBody)
                Body = httpBody;
            else
            {
                Body = new JsonBody(body);
            }
        }



        public void WriteToSocket(Socket socket)
        {
            var bytes = Encoding.ASCII.GetBytes($"HTTP/1.1 {(int)StatusCode} {StatusName}\r\n");
            socket.Send(bytes);

            for (int i = 0; i < Header.DefaultHeaders.Count; i++)
            {
                socket.Send(Header.DefaultHeaders[i].Bytes);

            }

            foreach (var header in Headers)
            {
                socket.Send(header.Bytes);
            }

            socket.Send(new Header("Date", DateTime.Now.ToString("R")).Bytes);

            if (Body != null)
            {
                socket.Send(new Header("Content-Length", Body.ContentLength.ToString()).Bytes);
                socket.Send(Body.ContentTypeHeader.Bytes);
            }
            else socket.Send(Header.EmptyBodyHeader.Bytes);

            socket.Send(newLine);


            if (Body != null)
            {
                socket.Send(Body.Bytes);
            }

            socket.Send(newLine);



        }


    }
}
