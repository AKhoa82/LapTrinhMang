using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HD6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            EndPoint tmpRemote = new IPEndPoint(IPAddress.Any, 0);

            int i = 10; 
            byte[] data;
            int recv;

            Console.WriteLine("Nhập dữ liệu (exit để thoát):");
            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine() ?? string.Empty;
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                client.SendTo(Encoding.UTF8.GetBytes(input), serverEP);

                data = new byte[i];
                try
                {
                    recv = client.ReceiveFrom(data, ref tmpRemote);
                    string msg = Encoding.UTF8.GetString(data, 0, recv);
                    Console.WriteLine($"Server: {msg}");
                }
                catch (SocketException)
                {
                    Console.WriteLine("Cảnh báo: dữ liệu bị mất, thử tăng bộ đệm và gửi lại...");
                    i += 10;
                }
            }

            client.Close();
        }
    }
}
