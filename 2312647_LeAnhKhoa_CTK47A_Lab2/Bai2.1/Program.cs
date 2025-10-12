using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bai2._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 5000);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(serverEndPoint);
            serverSocket.Listen(10);

            Console.WriteLine("Server đang chạy, chờ client kết nối...");

            Socket clientSocket = serverSocket.Accept();

            EndPoint clientEndPoint = clientSocket.RemoteEndPoint;
            Console.WriteLine("Client đã kết nối từ: " + clientEndPoint.ToString());

            byte[] buff;
            string hello = "Hello Client";
            buff = Encoding.ASCII.GetBytes(hello);
            clientSocket.Send(buff, 0, buff.Length, SocketFlags.None);

            Console.WriteLine("Đã gửi: " + hello);

            Console.ReadLine();
        }
    }
}
