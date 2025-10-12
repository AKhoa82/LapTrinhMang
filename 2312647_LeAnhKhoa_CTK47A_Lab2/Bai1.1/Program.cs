using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Bai1._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 5000);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(serverEndPoint);
            serverSocket.Listen(10);
            Socket clientSocket = serverSocket.Accept();

            EndPoint clientEndPoint = clientSocket.RemoteEndPoint;
            Console.WriteLine(clientEndPoint.ToString());
        }
    }
}
