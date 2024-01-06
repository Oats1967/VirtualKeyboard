using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;


namespace TCPTest;

public class Program
{

    public async static Task Main(string[] args)
    {


        
        IPAddress ip = IPAddress.Loopback; // Use loopback address for local host
        IPEndPoint iPEndPoint = new IPEndPoint(ip, 2000);

        // client socket
        using Socket client = new(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        await client.ConnectAsync(iPEndPoint);

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
                    await client.SendAsync(TKSetSize(), SocketFlags.None);
                    break; 
                case ConsoleKey.D2:
                    await client.SendAsync(TKSetShowPoint(), SocketFlags.None);
                    break;
                case ConsoleKey.D3:
                    await client.SendAsync(TKSetShow(), SocketFlags.None);
                    break;
                case ConsoleKey.D4:
                    await client.SendAsync(TKSetHide(), SocketFlags.None);
                    break;
            }

           
        }
    }
    private static ArraySegment<byte> TKSetHide()
    {
        ConsoleKeyInfo userInput;
        Console.WriteLine();
        Console.WriteLine("Press enter to hide keyboard.");
        while ((userInput = Console.ReadKey()).Key != ConsoleKey.Enter)
        {
            Console.WriteLine("\nInvalid input. Please press enter.");
        }
        Console.WriteLine("\n");
        var byteStream = new ArraySegment<byte>(new byte[9]);
        byteStream[0] = 0x02;
        byteStream[1] = 2;
        byteStream[2] = 0x15;
        

        return byteStream;
    }
    private static ArraySegment<byte> TKSetShow()
    {
        ConsoleKeyInfo userInput;
        Console.WriteLine();
        Console.WriteLine("Choose keyboard to show at desired position." +
            "\n0 for NumericKeyboard" +
            "\n1 for GermanKeyboard" +
            "\n2 for Not used");
        while (((userInput = Console.ReadKey()).Key != ConsoleKey.D0) && (userInput.Key != ConsoleKey.D1) && (userInput.Key != ConsoleKey.D2))
        {
            Console.WriteLine("\nInvalid input. Please enter '0' or '1' or '2'.");
        }
        var byteStream = new ArraySegment<byte>(new byte[4]);
        byteStream[0] = 0x02;
        byteStream[1] = 3;
        byteStream[2] = 0x14;
        byteStream[3] = Convert.ToByte(int.Parse(userInput.KeyChar.ToString()));
        Console.WriteLine("\n");

        Console.WriteLine("Press enter to show keyboard.");
        while ((userInput = Console.ReadKey()).Key != ConsoleKey.Enter)
        {
            Console.WriteLine("\nInvalid input. Please press enter.");
        }
        Console.WriteLine("\n");

        return byteStream;
    }
    private static ArraySegment<byte> TKSetShowPoint()
    {
        ConsoleKeyInfo userInput;
        Console.WriteLine();
        Console.WriteLine("Let's configure your keyboard! Press enter to continue.");
        while ((userInput = Console.ReadKey()).Key != ConsoleKey.Enter)
        {
            Console.WriteLine("\nInvalid input. Please press enter.");
        }
        Console.WriteLine("\n");

        // no config needed
        var byteStream = new ArraySegment<byte>(new byte[9]);
        byteStream[0] = 0x02;
        byteStream[1] = 7;
        byteStream[2] = 0x13;


        // Layout
        Console.WriteLine("Please choose your keyboard layout! " +
            "\n0 for NumericKeyboard" +
            "\n1 for GermanKeyboard" +
            "\n2 for Not used");
        while (((userInput = Console.ReadKey()).Key != ConsoleKey.D0) && (userInput.Key != ConsoleKey.D1) && (userInput.Key != ConsoleKey.D2))
        {
            Console.WriteLine("\nInvalid input. Please enter '0' or '1' or '2'.");
        }
        byteStream[3] = Convert.ToByte(int.Parse(userInput.KeyChar.ToString()));
        Console.WriteLine("\n");

        // X-Coordinate
        const int lowerLimitX = 0;
        const int upperLimitX = 1920;
        Console.WriteLine($"Please choose your keyboard X-Coordinate! Please enter a number between {lowerLimitX} and {upperLimitX}");
        int x;
        while (!(int.TryParse(Console.ReadLine(), out x) && x >= lowerLimitX && x <= upperLimitX))
        {
            Console.WriteLine($"Invalid input. Please enter a number between {lowerLimitX} and {upperLimitX}");
        }
        byte[] xBytes = BitConverter.GetBytes(x);
        byteStream[4] = xBytes[0];
        byteStream[5] = xBytes[1]; 
        Console.WriteLine();

        //Y - Coordinate
        const int lowerLimitY = 0;
        const int upperLimitY = 1080;
        Console.WriteLine($"Please choose your keyboard X-Coordinate! Please enter a number between {lowerLimitY} and {upperLimitY}");
        int y;
        while (!(int.TryParse(Console.ReadLine(), out y) && y >= lowerLimitY && y <= upperLimitY))
        {
            Console.WriteLine($"Invalid input. Please enter a number between {lowerLimitY} and {upperLimitY}");
        }
        byte[] yBytes = BitConverter.GetBytes(y);
        byteStream[6] = yBytes[0];
        byteStream[7] = yBytes[1]; 
        Console.WriteLine();


        byteStream[8] = 0x03;


        return byteStream;
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
        Console.WriteLine("Please choose your keyboard layout! " +
            "\n0 for NumericKeyboard" +
            "\n1 for GermanKeyboard" +
            "\n2 for Not used");
        while (((userInput = Console.ReadKey()).Key != ConsoleKey.D0) && (userInput.Key != ConsoleKey.D1) && (userInput.Key != ConsoleKey.D2))
        {
            Console.WriteLine("\nInvalid input. Please enter '0' or '1' or '2'.");
        }
        byteStream[3] = Convert.ToByte(int.Parse(userInput.KeyChar.ToString()));
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