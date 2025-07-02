using System.Net;
using System.Net.Sockets;


namespace TCPTest;

public class Program
{


    private static readonly Dictionary<string, ArraySegment<byte>> Commands = new()
    {

        // TKSetSize
        ["TKSetSize Numeric 100%"] = new ArraySegment<byte>([0x02, 7, 0x11, 1, 0x64, 0, 0, 0, 0x03]),
        ["TKSetSize Numeric 75%"] = new ArraySegment<byte>([0x02, 7, 0x11, 1, 0x4b, 0, 0, 0, 0x03]),
        ["TKSetSize Numeric 50%"] = new ArraySegment<byte>([0x02, 7, 0x11, 1, 0x32, 0, 0, 0, 0x03]),

        ["TKSetSize German 100%"] = new ArraySegment<byte>([0x02, 7, 0x11, 2, 0x64, 0, 0, 0, 0x03]),
        ["TKSetSize German 75%"] = new ArraySegment<byte>([0x02, 7, 0x11, 2, 0x4b, 0, 0, 0, 0x03]),
        ["TKSetSize German 50%"] = new ArraySegment<byte>([0x02, 7, 0x11, 2, 0x32, 0, 0, 0, 0x03]),

        ["TKSetSize English 100%"] = new ArraySegment<byte>([0x02, 7, 0x11, 3, 0x64, 0, 0, 0, 0x03]),
        ["TKSetSize English 75%"] = new ArraySegment<byte>([0x02, 7, 0x11, 3, 0x4b, 0, 0, 0, 0x03]),
        ["TKSetSize English 50%"] = new ArraySegment<byte>([0x02, 7, 0x11, 3, 0x32, 0, 0, 0, 0x03]),

        ["TKSetSize Dutch 100%"] = new ArraySegment<byte>([0x02, 7, 0x11, 4, 0x64, 0, 0, 0, 0x03]),
        ["TKSetSize Dutch 75%"] = new ArraySegment<byte>([0x02, 7, 0x11, 4, 0x4b, 0, 0, 0, 0x03]),
        ["TKSetSize Dutch 50%"] = new ArraySegment<byte>([0x02, 7, 0x11, 4, 0x32, 0, 0, 0, 0x03]),

        ["TKSetSize Not used 100%"] = new ArraySegment<byte>([0x02, 7, 0x11, 5, 0x64, 0, 0, 0, 0x03]),
        ["TKSetSize Not used 75%"] = new ArraySegment<byte>([0x02, 7, 0x11, 5, 0x4b, 0, 0, 0, 0x03]),
        ["TKSetSize Not used 50%"] = new ArraySegment<byte>([0x02, 7, 0x11, 5, 0x32, 0, 0, 0, 0x03]),

        // TKSetShowPoint
        ["TKSetShowPoint Numeric 0 0"] = new ArraySegment<byte>([0x02, 7, 0x13, 1, 0, 0, 0, 0, 0x03]),
        ["TKSetShowPoint Numeric 400 400"] = new ArraySegment<byte>([0x02, 7, 0x13, 1, 0x90, 0x01, 0x90, 0x01, 0x03]),

        ["TKSetShowPoint German 0 0"] = new ArraySegment<byte>([0x02, 7, 0x13, 2, 0, 0, 0, 0, 0x03]),
        ["TKSetShowPoint German 400 400"] = new ArraySegment<byte>([0x02, 7, 0x13, 2, 0x90, 0x01, 0x90, 0x01, 0x03]),

        ["TKSetShowPoint English 0 0"] = new ArraySegment<byte>([0x02, 7, 0x13, 3, 0, 0, 0, 0, 0x03]),
        ["TKSetShowPoint English 400 400"] = new ArraySegment<byte>([0x02, 7, 0x13, 3, 0x90, 0x01, 0x90, 0x01, 0x03]),

        ["TKSetShowPoint Dutch 0 0"] = new ArraySegment<byte>([0x02, 7, 0x13, 4, 0, 0, 0, 0, 0x03]),
        ["TKSetShowPoint Dutch 400 400"] = new ArraySegment<byte>([0x02, 7, 0x13, 4, 0x90, 0x01, 0x90, 0x01, 0x03]),

        ["TKSetShowPoint Not used 0 0"] = new ArraySegment<byte>([0x02, 7, 0x13, 5, 0, 0, 0, 0, 0x03]),
        ["TKSetShowPoint Not used 400 400"] = new ArraySegment<byte>([0x02, 7, 0x13, 5, 0x90, 0x01, 0x90, 0x01, 0x03]),

        // TKSetShow
        ["TKSetShow Numeric"] = new ArraySegment<byte>([0x02, 3, 0x14, 1, 0x03]),
        ["TKSetShow German"] = new ArraySegment<byte>([0x02, 3, 0x14, 2, 0x03]),
        ["TKSetShow English"] = new ArraySegment<byte>([0x02, 3, 0x14, 3, 0x03]),
        ["TKSetShow Dutch"] = new ArraySegment<byte>([0x02, 3, 0x14, 4, 0x03]),
        ["TKSetShow Not used"] = new ArraySegment<byte>([0x02, 3, 0x14, 5, 0x03]),

        // TKSetHide
        ["TKSetHide"] = new ArraySegment<byte>([0x02, 2, 0x15, 0x03]),
    };





    

    public async static Task Main(string[] args)
    {
        
        var logger = new ProcessLogger( "VirtualKeyboard", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LogFile.txt"), TimeSpan.FromMinutes(30)); 
        logger.Start();
        var ip = IPAddress.Loopback; // Use loopback address for local host
        var iPEndPoint = new IPEndPoint(ip, 2000);

        // client socket
        using var client = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        await client.ConnectAsync(iPEndPoint);
        var random = new Random();
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddHours(24);
        /*while (DateTime.Now < endTime)
        {

            var command = Commands[random.Next(0, Commands.Count)];
            Console.WriteLine($"{DateTime.Now}: {command.commandMessage}");
            await client.SendAsync(command.byteStream, SocketFlags.None);
            Thread.Sleep(1000);


        }*/
        Console.Write("German - PRESS KEY TO CONTINUE");
        Console.ReadKey();
        await client.SendAsync(Commands["TKSetShowPoint German 400 400"], SocketFlags.None);

        Console.Write("English - PRESS KEY TO CONTINUE");
        Console.ReadKey();
        await client.SendAsync(Commands["TKSetShowPoint English 400 400"], SocketFlags.None);

        Console.Write("Dutch - PRESS KEY TO CONTINUE");
        Console.ReadKey();
        await client.SendAsync(Commands["TKSetShowPoint Dutch 400 400"], SocketFlags.None);


        Console.Write("German - PRESS KEY TO CONTINUE");
        Console.ReadKey();
        await client.SendAsync(Commands["TKSetShowPoint German 400 400"], SocketFlags.None);

        Console.Write("English - PRESS KEY TO CONTINUE");
        Console.ReadKey();
        await client.SendAsync(Commands["TKSetShowPoint English 400 400"], SocketFlags.None);

        Console.Write("Dutch - PRESS KEY TO CONTINUE");
        Console.ReadKey();
        await client.SendAsync(Commands["TKSetShowPoint Dutch 400 400"], SocketFlags.None);



        Console.ReadKey();

        logger.Stop();
    }

}