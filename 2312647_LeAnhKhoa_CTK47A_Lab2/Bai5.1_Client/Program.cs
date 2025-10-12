using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bai6._1_Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Đang kết nối với server...");
            try
            {
                serverSocket.Connect(serverEndPoint);
            }
            catch (SocketException)
            {
                Console.WriteLine("Không thể kết nối đến server");
                return;
            }

            if (serverSocket.Connected)
            {
                Console.WriteLine("Kết nối thành công với server...");

                // Nhận lời chào ban đầu
                byte[] buff = new byte[1024];
                int byteReceive = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
                string str = Encoding.ASCII.GetString(buff, 0, byteReceive);
                Console.WriteLine(str);

                // Bắt đầu vòng lặp gửi/nhận
                while (true)
                {
                    Console.Write("Nhập tin nhắn (gõ 'exit' để thoát): ");
                    string input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input)) continue;
                    if (input.ToLower() == "exit") break;

                    // Gửi dữ liệu
                    byte[] sendBuff = Encoding.ASCII.GetBytes(input);
                    serverSocket.Send(sendBuff, 0, sendBuff.Length, SocketFlags.None);

                    // Nhận phản hồi từ server
                    buff = new byte[1024];
                    byteReceive = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
                    str = Encoding.ASCII.GetString(buff, 0, byteReceive);
                    Console.WriteLine("Server trả về: " + str);
                }
            }

            try { serverSocket.Shutdown(SocketShutdown.Both); } catch { }
            serverSocket.Close();
        }
    }
}
