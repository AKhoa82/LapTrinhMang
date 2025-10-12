using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace HD3_Server
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

            while (true)
            {
                byte[] buffer = new byte[1024];
                int bytesReveived = serverSocket.ReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref remote);
                string str = Encoding.UTF8.GetString(buffer, 0, bytesReveived);
                Console.WriteLine($"[{remote}] {str}");

                serverSocket.SendTo(buffer, 0, bytesReveived, SocketFlags.None, remote);
                if (str.Equals("exit all", StringComparison.OrdinalIgnoreCase)) break;
            }
            Console.WriteLine("Đang tắt server ...");
            serverSocket.Close();
        }
    }
}
