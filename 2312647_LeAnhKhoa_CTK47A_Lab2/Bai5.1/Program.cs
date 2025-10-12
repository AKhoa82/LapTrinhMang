using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bai5._1_Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 5000);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(ep);
            listener.Listen(10);

            Console.WriteLine("Server đang chạy, chờ client kết nối...");

            while (true)
            {
                Socket client = listener.Accept();
                Console.WriteLine("Client đã kết nối từ: " + client.RemoteEndPoint);

                // Gửi lời chào ban đầu
                string hello = "Hello Client";
                byte[] buff = Encoding.ASCII.GetBytes(hello);
                client.Send(buff, 0, buff.Length, SocketFlags.None);

                // Bắt đầu vòng lặp trao đổi dữ liệu
                while (true)
                {
                    try
                    {
                        byte[] recvBuff = new byte[1024];
                        int n = client.Receive(recvBuff, 0, recvBuff.Length, SocketFlags.None);
                        if (n <= 0) break; // client đã đóng

                        string msg = Encoding.ASCII.GetString(recvBuff, 0, n);
                        Console.WriteLine("Nhận từ client: " + msg);

                        // Gửi lại cho client (echo)
                        client.Send(recvBuff, 0, n, SocketFlags.None);
                    }
                    catch
                    {
                        break;
                    }
                }

                Console.WriteLine("Client ngắt kết nối.");
                client.Close();
            }
        }
    }
}
