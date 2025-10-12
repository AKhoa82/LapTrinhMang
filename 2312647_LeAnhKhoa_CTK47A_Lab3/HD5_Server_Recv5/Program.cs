using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HD5_Server_Recv5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            s.Bind(ep);
            Console.WriteLine($"Server nhận 5 thông điệp tại {ep}");

            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            byte[] buf = new byte[1024];

            for (int i = 1; i <= 5; i++)
            {
                int n = s.ReceiveFrom(buf, 0, buf.Length, SocketFlags.None, ref remote);
                string str = Encoding.UTF8.GetString(buf, 0, n);
                Console.WriteLine($"#{i} từ {remote}: {str}");
            }

            Console.WriteLine("Xong 5 gói. Nhấn Enter để thoát...");
            Console.ReadLine();
        }
    }
}
