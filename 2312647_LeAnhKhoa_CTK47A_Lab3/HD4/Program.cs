using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HD4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IPEndPoint remote = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            client.Connect(remote);
            Console.WriteLine($"Đã Connect tới {remote}");

            Console.WriteLine("Nhập nội dung (exit để thoát):");
            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine() ?? string.Empty;
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

                byte[] sendBuf = Encoding.UTF8.GetBytes(input);
                client.Send(sendBuf);

                byte[] recvBuf = new byte[1024];
                int bytes = client.Receive(recvBuf);
                Console.WriteLine("Server: {0}", Encoding.UTF8.GetString(recvBuf, 0, bytes));
            }

            client.Close();
        }
    }
}
