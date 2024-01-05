﻿using System.Net;
using System.Net.Sockets;
using System.Text;


namespace TCPTest;

public class Program
{

    public async static Task Main(string[] args)
    {


        IPHostEntry ipEntry = await Dns.GetHostEntryAsync(Dns.GetHostName());
        IPAddress ip = ipEntry.AddressList[0];
        IPEndPoint iPEndPoint = new(ip, 1234);

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

            var responseBuffer = new byte[1024];
            var received = await client.ReceiveAsync(responseBuffer, SocketFlags.None);

            var answer = Encoding.UTF8.GetString(responseBuffer, 0, received);

            Console.WriteLine(answer);
        }
    }

    private static ArraySegment<byte> TKSetHide()
    {
        throw new NotImplementedException();
    }

    private static ArraySegment<byte> TKSetShow()
    {
        ConsoleKeyInfo userInput;
        Console.WriteLine();
        Console.WriteLine("Press enter to show keyboard at desired position.");
        while ((userInput = Console.ReadKey()).Key != ConsoleKey.Enter)
        {
            Console.WriteLine("\nInvalid input. Please press enter.");
        }
        Console.WriteLine("\n");
        var byteStream = new ArraySegment<byte>(new byte[9]);
        byteStream[0] = 0x02;
        byteStream[1] = 3;
        byteStream[2] = 0x14;
        byteStream[3] = 0;

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