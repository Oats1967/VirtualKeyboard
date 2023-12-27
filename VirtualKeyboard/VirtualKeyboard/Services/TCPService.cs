using Microsoft.Extensions.Logging;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Services
{
    public class TCPService : ITCPService
    {
        ILogger _logger;
        public TCPService(ILogger<TCPService> logger)
        {
           _logger = logger;
        }

        public async void StartAsync()
        {
             // info abut localhost -- includes ip adress
             IPHostEntry ipEntry = await Dns.GetHostEntryAsync(Dns.GetHostName());
             // localhost ip
             IPAddress ip = ipEntry.AddressList[0];;
             // connects server socket to client socket
             IPEndPoint ipEndPoint = new IPEndPoint(ip, 1234);
             using Socket server = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
             server.Bind(ipEndPoint);
             server.Listen();
            _logger.LogInformation("Server started listening");

            var handler = await server.AcceptAsync();

            while(true)
            {
                var buffer = new byte[1024];
                var received = await handler.ReceiveAsync(buffer, SocketFlags.None);

                var message = Encoding.UTF8.GetString(buffer, 0, received);
                if(message != null)
                {
                    _logger.LogInformation($"Server received message: {message}");
                    var response = "Server received message";
                    var responseByte = Encoding.UTF8.GetBytes(response);
                    await handler.SendAsync(responseByte, SocketFlags.None);
                }
            }
        }

    }
}
