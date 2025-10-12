using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bai3._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; // để in tiếng Việt

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Đang kết nối với server...");

            try
            {
                serverSocket.Connect(serverEndPoint);
            }
            catch
            {
                Console.WriteLine("Không thể kết nối đến server");
                return;
            }
            //serverSocket.Connect(serverEndPoint);

            if (serverSocket.Connected)
            {
                Console.WriteLine("Kết nối thành công với server...");

                byte[] buff = new byte[1024];
                int byteReceive = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);

                if (byteReceive > 0)
                {
                    string str = Encoding.ASCII.GetString(buff, 0, byteReceive);
                    Console.WriteLine(str);
                }
                else
                    Console.WriteLine("Không nhận được dữ liệu từ server.");
            }
            Console.ReadLine();
        }
    }
}
