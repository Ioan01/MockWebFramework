using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.HttpExceptions;
using MockWebFramework.Networking.Http.Response;
using MockWebFramework.Networking.HttpRequest;

namespace MockWebFramework.Networking
{
    internal class TcpHost
    {
        private readonly int _port = 80;

        // buffers will be allocated at startup to minimize allocation and gc overhead
        // buffer size for small requests
        private readonly int _maxPacketSizeSmall = 4 * 1024;

        // buffer size for medium size requests
        
        private readonly int _maxPacketSizeMedium = 4 * 1024 * 1024;

        // absolute maximum size for packets
        // no buffers will be allocated to keep memory consumption down, they will be allocated when needed
        private readonly int _maxPacketSizeLarge = 1024 * 1024 * 256;

        private readonly int _maxSmallBuffers = 100;
        private readonly int _maxMediumBuffers = 5;

        private int _currentClients = 0;

        private Buffer[] mediumBuffers;


        private Buffer[] smallBuffers;

        public event EventHandler<RequestReceivedEvent> PacketReceivedEvent; 


        private TcpListener _listener;

        

        

        public TcpHost()
        {
            _listener = TcpListener.Create(80);
            _listener.Start(10);
            mediumBuffers = new Buffer[_maxMediumBuffers];
            smallBuffers = new Buffer[_maxSmallBuffers];
            for (int i = 0; i < _maxMediumBuffers; i++)
            {
                mediumBuffers[i] = new Buffer(_maxPacketSizeMedium);
            }

            for (int i = 0; i < _maxSmallBuffers; i++)
            {
                smallBuffers[i] = new Buffer(_maxPacketSizeSmall);
            }
        }

        public async Task Listen()
        {
            try
            {
                while (true)
                {
                    if (_listener.Pending())
                    {
                        var socket = await _listener.AcceptSocketAsync();
                        ThreadPool.QueueUserWorkItem(_ => HandleClient(socket));
                        Thread.Sleep(1);
                    }
                }
            }
            catch (Exception e)
            {
                await Listen();
            }
            
        }

        private void HandleClient(Socket clientSocket)
        {

            HttpResponse response = new HttpResponse(0,string.Empty);
            try
            {

                Buffer? freeBuffer = null;

                if (clientSocket.Available > _maxPacketSizeLarge)
                {
                    throw new ContentTooLargeException();
                }

                // allocate large buffer
                if (clientSocket.Available > _maxPacketSizeMedium)
                    freeBuffer = new Buffer(clientSocket.Available);

                // wait for other buffers to be freed
                while (freeBuffer == null)
                {
                    if (clientSocket.Available > _maxPacketSizeSmall)
                        freeBuffer = mediumBuffers.FirstOrDefault(b => b.Free);
                    else freeBuffer = smallBuffers.FirstOrDefault(b => b.Free);

                    if (freeBuffer != null)
                        freeBuffer.Free = false;
                }

                clientSocket.Receive(freeBuffer.ArraySegment.Span, SocketFlags.None);





                var request = new HttpRequest.HttpRequest(freeBuffer.ArraySegment);

                var @event = new RequestReceivedEvent(request, clientSocket);

                PacketReceivedEvent?.Invoke(this, @event);
                response = @event.Response;

                freeBuffer.Free = true;
            }
            catch (HttpException e)
            {
                response = new HttpResponse(e.Code, e.Name);
            }
            catch (Exception e)
            {
                // 500
                response = new HttpResponse(500, "Internal Server Error");
            }
            finally
            {
                if (clientSocket.Connected)
                {
                    if (response == null)
                        response = new HttpResponse(204, "No content");
                    response.WriteToSocket(clientSocket);
                }
                clientSocket.Close();
            }

        }

        
    }
}
