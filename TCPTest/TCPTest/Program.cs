using System.Net;
using System.Net.Sockets;
using System.Text;


namespace TCPTest;

public class Program
{

    public async static Task Main(string[] args)
    {

    
        //IPHostEntry ipEntry = await Dns.GetHostEntryAsync(Dns.GetHostName());
        //IPAddress ip = ipEntry.AddressList[0];
        //IPEndPoint iPEndPoint = new(ip,1234);

        //// client socket
        //using Socket client = new(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        //await client.ConnectAsync(iPEndPoint);

        while (true)
        {
            Console.WriteLine("Please type in a number for your command.\n" +
                "1 for TKSetSize\n" +
                 "2 for TKSetShowPoint\n" +
                 "3 for TKSetShow\n" +
                 "4 for TKSetHide\n");
            ConsoleKeyInfo userInput;
            while ((userInput = Console.ReadKey()).Key != ConsoleKey.D1 && (userInput.Key != ConsoleKey.D2) && (userInput.Key != ConsoleKey.D3) && (userInput.Key != ConsoleKey.D4))
            {
                Console.WriteLine("\nInvalid input. Please type in a number for your command.\n" +
                "1 for TKSetSize\n" +
                 "2 for TKSetShowPoint\n" +
                 "3 for TKSetShow\n" +
                 "4 for TKSetHide\n");
            }
             Console.WriteLine();
            ArraySegment<byte> buffer;
            switch (userInput.Key)
            {
                case ConsoleKey.D1:
                    buffer = TKSetSize();
                    break; 
                case ConsoleKey.D2:
                    buffer = TKSetShowPoint();
                    break;
                case ConsoleKey.D3:
                    buffer = TKSetShow();
                    break;
                case ConsoleKey.D4:
                    buffer = TKSetHide();
                    break;
            }
           



            //await client.SendAsync(byteStream, SocketFlags.None);

            //var buffer = new byte[1024];
            //var received = await client.ReceiveAsync(buffer, SocketFlags.None);

            //var answer = Encoding.UTF8.GetString(buffer, 0, received);

            //Console.WriteLine(answer);
        }
    }

    private static ArraySegment<byte> TKSetHide()
    {
        throw new NotImplementedException();
    }

    private static ArraySegment<byte> TKSetShow()
    {
        throw new NotImplementedException();
    }

    private static ArraySegment<byte> TKSetShowPoint()
    {
        throw new NotImplementedException();
    }

    private static ArraySegment<byte> TKSetSize()
    {

        ConsoleKeyInfo userInput;
        Console.WriteLine();
        Console.WriteLine("Let's configure your keyboard! Press enter to continue.");
        while((userInput = Console.ReadKey()).Key != ConsoleKey.Enter)
        {
            Console.WriteLine("\nInvalid input. Please press enter.");
        }
        Console.WriteLine("\n");

        // no config needed
        var byteStream = new ArraySegment<byte>(new byte[9]);
        byteStream[0] = 0x02;
        byteStream[1] = 7;
        byteStream[2] = 0x11;


        // Layout
        Console.WriteLine("Please choose your keyboard layout! Press 1 for numeric and 2 for alphabetic to continue.");
        while (((userInput = Console.ReadKey()).Key != ConsoleKey.D1) && (userInput.Key != ConsoleKey.D2))
        {
            Console.WriteLine("\nInvalid input. Please enter '1' or '2'.");
        }
        byteStream[3] = Convert.ToByte(userInput.KeyChar);
        Console.WriteLine("\n");


        // percentage
        const int lowerLimit = 0;
        const int upperLimit = 100;
        Console.WriteLine($"Please choose your keyboard size percentage! Please enter a number between {lowerLimit} and {upperLimit}");
        int percentage;
        while (!(int.TryParse(Console.ReadLine(), out percentage) && percentage >= lowerLimit && percentage <= upperLimit))
        {
            Console.WriteLine($"Invalid input. Please enter a number between {lowerLimit} and {upperLimit}");
        }
        byte[] percentageBytes = BitConverter.GetBytes(percentage);
        byteStream[4] = percentageBytes[0];
        byteStream[5] = 0; // check if neccessary
        Console.WriteLine();
      
        byteStream[8] = 0x03;
        

        return byteStream;
    }
}