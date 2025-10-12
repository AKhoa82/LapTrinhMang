using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;

namespace HD1_UDP_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("Nhập nội dung (exit = thoát client, exit all = tắt cả client và server):");

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine() ?? string.Empty;

                byte[] outBuf = Encoding.UTF8.GetBytes(input);
                clientSocket.SendTo(outBuf, 0, outBuf.Length, SocketFlags.None, serverEndPoint);

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Client thoát...");
                    break;
                }

                if (input.Equals("exit all", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Đã gửi lệnh tắt server, client thoát...");
                    break;
                }
                byte[] inBuf = new byte[1024];
                int bytes = clientSocket.ReceiveFrom(inBuf, 0, inBuf.Length, SocketFlags.None, ref remote);
                string echo = Encoding.UTF8.GetString(inBuf, 0, bytes);
                Console.WriteLine($"Server: {echo}");
            }
            clientSocket.Close();
        }
    }
}
