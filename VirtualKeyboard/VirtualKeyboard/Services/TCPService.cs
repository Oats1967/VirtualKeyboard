using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace VirtualKeyboard.Services
{
   
    public class TCPService : ITCPService
    {
        ILogger _logger;
        IProcessMessageService _messageService;
        public TCPService(ILogger<TCPService> logger, IProcessMessageService messageService)
        {
           _logger = logger;
            _messageService = messageService;
            StartAsync();
        }



        private async void StartAsync()
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
            try
            {
                while (true)
                {
                    var buffer = new byte[1024];
                    var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
                    if (received > 0)
                    {
                        // Process the received data in the buffer (up to 'received' bytes)
                        byte[] receivedData = new byte[received];
                        Array.Copy(buffer, receivedData, received);
                        _logger.LogInformation($"Server received message");
                        _messageService.Process(receivedData);
                        var response = "Server received message";
                        var responseByte = Encoding.UTF8.GetBytes(response);
                        await handler.SendAsync(responseByte, SocketFlags.None);
                        // Handle/process receivedData as needed
                    }
                }
            }
            catch(SocketException ex)
            {
                _logger.LogError($"Client disconnected: {ex.Message}");
            }
            finally
            {
                // Additional cleanup or logging if needed
                handler.Close();
            }
        }

       
    }
}
