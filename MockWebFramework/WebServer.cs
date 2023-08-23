using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Networking;
using MockWebFramework.Controller;
using MockWebFramework.Logging;
using MockWebFramework.Service;

namespace MockWebFramework
{
    internal class WebServer
    {
        private TcpHost _tcpHost = new TcpHost();

        public ServiceHost Services { get; }

        public ControllerHost Controllers { get; }

        public WebServer()
        {

            Services = new ServiceHost();
            Controllers = new ControllerHost(Services.ServiceContainer);

            _tcpHost.PacketReceivedEvent += (o, @event) => Controllers.HandleRequest(@event);


        }



        public async Task Start()
        {
            if (Ilogger.Instance.LoggedErrorsOrFatals)
                return;

            await _tcpHost.Listen();
        }
    }
}
