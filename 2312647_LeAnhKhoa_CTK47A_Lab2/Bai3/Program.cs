using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BaiTap3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                Console.WriteLine("Đang kết nối CalcServer ...");
                sock.Connect(serverEndPoint);
                Console.WriteLine("Kết nối thành công.");

                var buff = new byte[1024];
                int n = sock.Receive(buff);
                Console.WriteLine("Server: " + Encoding.ASCII.GetString(buff, 0, n));

                while (true)
                {
                    Console.Write("Nhập phép tính (vd: 10 + 3) hoặc 'exit': ");
                    string s = Console.ReadLine() ?? "";

                    byte[] data = Encoding.ASCII.GetBytes(s);
                    sock.Send(data);

                    if (s.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Thoát client.");
                        break;
                    }

                    n = sock.Receive(buff);
                    Console.WriteLine("Kết quả từ server: " + Encoding.ASCII.GetString(buff, 0, n));
                }
            }
            catch (SocketException)
            {
                Console.WriteLine("Không thể kết nối đến server.");
            }
            finally
            {
                try { sock.Shutdown(SocketShutdown.Both); } catch { }
                try { sock.Close(); } catch { }
            }
        }
    }
}
