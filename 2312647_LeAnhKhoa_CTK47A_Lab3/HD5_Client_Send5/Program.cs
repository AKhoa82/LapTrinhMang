using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HD5_Client_Send5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IPEndPoint server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            for (int i = 1; i <= 5; i++)
            {
                string str = "Thông Điệp " + i.ToString();
                byte[] buff = Encoding.UTF8.GetBytes(str);
                sock.SendTo(buff, 0, buff.Length, SocketFlags.None, server);
                Console.WriteLine($"Đã gửi: {str}");
            }
        }
    }
}
