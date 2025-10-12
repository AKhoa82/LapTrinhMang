using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HD3_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("Nhập nội dung (exit = thoát, 'exit all' = yêu cầu server thoát):");

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine() ?? string.Empty;
                byte[] outBuf = Encoding.UTF8.GetBytes(input);

                clientSocket.SendTo(outBuf, 0, outBuf.Length, SocketFlags.None, serverEndPoint);

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

                byte[] buff = new byte[1024];
                int bytes = clientSocket.ReceiveFrom(buff, 0, buff.Length, SocketFlags.None, ref remote);
                string echo = Encoding.ASCII.GetString(buff, 0, bytes);
                Console.WriteLine($"Server: {echo}");
            }
            Console.WriteLine("Đang tắt client...");
            clientSocket.Close();
        }
    }
}
