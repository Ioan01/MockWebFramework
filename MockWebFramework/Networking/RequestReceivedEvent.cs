using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Http;
using MockWebFramework.Http.Response;

namespace MockWebFramework.Networking
{
    internal class RequestReceivedEvent
    {
        public HttpRequest Request { get; }

        public Socket ClientSocket { get; }

        public HttpResponse Response { get; set; }

        public RequestReceivedEvent(HttpRequest request,Socket clientSocket)
        {
            Request = request;
            ClientSocket = clientSocket;
        }

        
    }
}
