using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Networking.HttpRequest;
using MockWebFramework.Networking.HttpRequest.Body;

namespace MockWebFramework.Networking.Http
{
    internal class HttpResponse
    {
        public int StatusCode { get; }

        public string StatusName { get; }

        public List<Header> Headers { get; }

        public HttpBody? Body { get; }

        private static readonly byte[] newLine = { (byte)0xd, (byte)0xa };


        public HttpResponse(int statusCode, string statusName)
        {
            StatusCode = statusCode;
            StatusName = statusName;
        }

        public void WriteToSocket(Socket socket)
        {
            var bytes = Encoding.ASCII.GetBytes($"HTTP/1.1 {StatusCode} {StatusName}\r\n");
            socket.Send(bytes);

            for (int i = 0; i < Header.DefaultHeaders.Count; i++)
            {
                socket.Send(Header.DefaultHeaders[i].Bytes);

            }

            socket.Send(new Header("Date", DateTime.Now.ToString("R")).Bytes);
            socket.Send(new Header("Content-Length", 
                Body != null ? Body.ContentLength.ToString() : "0").Bytes);
            socket.Send(newLine);
            if (Body != null)
            {
                socket.Send(Body.Bytes);
            }

            socket.Send(newLine);



        }


    }
}
