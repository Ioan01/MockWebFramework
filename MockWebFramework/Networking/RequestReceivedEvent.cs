using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Networking
{
    internal class RequestReceivedEvent
    {
        public HttpRequest Request { get; }

        public Socket ClientSocket { get; }

        public RequestReceivedEvent(HttpRequest request, Socket clientSocket)
        {
            Request = request;
            ClientSocket = clientSocket;
        }
    }
}
