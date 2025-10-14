using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpEchoClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Parameter(s): <Server> <Port> <Data>");
                return;
            }

            string server = args[0];
            int port = int.Parse(args[1]);
            string message = args[2];

            try
            {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.Connect(new IPEndPoint(IPAddress.Parse(server), port));

                byte[] sendBuffer = Encoding.ASCII.GetBytes(message);
                int bytesSent = sock.Send(sendBuffer, 0, sendBuffer.Length, SocketFlags.None);

                Console.WriteLine($"Sent {bytesSent} bytes to server.");

                byte[] recvBuffer = new byte[32];
                int bytesRecv = 0;
                int totalBytesRecv = 0;
                string receivedData = "";

                while (totalBytesRecv < sendBuffer.Length)
                {
                    bytesRecv = sock.Receive(recvBuffer, 0, recvBuffer.Length, SocketFlags.None);
                    if (bytesRecv == 0) break;

                    receivedData += Encoding.ASCII.GetString(recvBuffer, 0, bytesRecv);
                    totalBytesRecv += bytesRecv;
                }

                Console.WriteLine("Received from server: " + receivedData);

                sock.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
