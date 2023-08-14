using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Networking;
using MockWebFramework.Controller;
using MockWebFramework.Service;

namespace MockWebFramework
{
    internal class WebServer
    {
        private TcpHost _tcpHost = new TcpHost();
        public ControllerHost Controllers { get; } = new ControllerHost();
        public ServiceHost Services { get; } = new ServiceHost();

        public ServerConfiguration Configuration { get; } = new ServerConfiguration();
        public WebServer()
        {
            _tcpHost.PacketReceivedEvent += OnPacketReceived;
        }

        private void OnPacketReceived(object? sender, RequestReceivedEvent e)
        {
            try
            {
                Controllers.HandleRequest(e);
            }
            catch (HttpRequestException httpException)
            {
                // print out code
                
                
            }
            catch (Exception exception)
            {
                // internal servor error
            }
            
        }

        public async Task Start()
        {
            await _tcpHost.Listen();
        }
    }
}
