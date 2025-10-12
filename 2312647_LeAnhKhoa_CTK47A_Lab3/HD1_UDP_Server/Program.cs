using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;

namespace HD1_UDP_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            serverSocket.Bind(serverEndPoint);
            Console.WriteLine($"UDP Server đang lắng nghe tại {serverEndPoint}");

            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            byte[] buffer = new byte[1024];

            while (true)
            {
                int bytesReceived = serverSocket.ReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref remote);
                string str = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                Console.WriteLine($"Nhận từ {remote}: {str}");

                serverSocket.SendTo(buffer, 0, bytesReceived, SocketFlags.None, remote);

                if (str.Equals("exit all", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Nhận lệnh tắt từ client. Đang tắt server...");
                    break;
                } 
            }
            serverSocket.Close();
        }
    }
}
