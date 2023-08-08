using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Networking;

namespace MockWebFramework
{
    internal class WebServer
    {
        private TcpHost _tcpHost = new TcpHost();
        private ServiceHost _serviceHost;
        


        public WebServer()
        {
            _tcpHost.PacketReceivedEvent += OnPacketReceived;
        }

        private void OnPacketReceived(object? sender, RequestReceivedEvent e)
        {
            
        }

        public async Task Start()
        {
            await _tcpHost.Listen();
        }
    }
}
