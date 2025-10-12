using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HD7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string input, stringData;
            int recv;

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            RetryUdpClient retryClient = new RetryUdpClient();

            int sockopt = (int)server.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
            Console.WriteLine($"Giá trị timeout mặc định: {sockopt}");

            server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);

            sockopt = (int)server.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
            Console.WriteLine($"Giá trị timeout mới: {sockopt}");

            string welcome = "Xin chào Server";
            byte[] data = Encoding.UTF8.GetBytes(welcome);
            recv = retryClient.SndRcvData(server, data, ipep);

            if (recv > 0)
            {
                stringData = Encoding.UTF8.GetString(data, 0, recv);
                Console.WriteLine(stringData);
            }
            else
            {
                Console.WriteLine("Không thể liên lạc với thiết bị ở xa.");
                return;
            }

            while (true)
            {
                Console.Write("> ");
                input = Console.ReadLine() ?? "";
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                recv = retryClient.SndRcvData(server, Encoding.UTF8.GetBytes(input), ipep);

                if (recv > 0)
                {
                    Console.WriteLine("Dữ liệu gửi thành công.");
                }
                else
                {
                    Console.WriteLine("Không nhận được câu trả lời từ server.");
                }
            }

            Console.WriteLine("Đang đóng client...");
            server.Close();
        }
    }
}
