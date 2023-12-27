using System.Net;
using System.Net.Sockets;
using System.Text;

IPHostEntry ipEntry = await Dns.GetHostEntryAsync(Dns.GetHostName());
IPAddress ip = ipEntry.AddressList[0];
IPEndPoint iPEndPoint = new(ip,1234);

// client socket
using Socket client = new(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

await client.ConnectAsync(iPEndPoint);

while (true)
{
    Console.WriteLine("Send a message:");
    var message = Console.ReadLine();

    var messageBytes = Encoding.UTF8.GetBytes(message!);

    await client.SendAsync(messageBytes, SocketFlags.None);
    var buffer = new byte[1024];

    var received = await client.ReceiveAsync(buffer, SocketFlags.None);

    var answer = Encoding.UTF8.GetString(buffer, 0, received);

    Console.WriteLine(answer);

}