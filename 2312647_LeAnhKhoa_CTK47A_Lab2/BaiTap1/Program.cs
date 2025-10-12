using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BaiTap1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 5000);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(ep);
            listener.Listen(10);
            Console.WriteLine("Server đang lắng nghe tại 127.0.0.1:5000 ...");

            while (true)
            {
                Socket client = null;
                try
                {
                    client = listener.Accept();
                    Console.WriteLine($"Kết nối từ {client.RemoteEndPoint}");

                    byte[] hello = Encoding.ASCII.GetBytes("Hello Client");
                    client.Send(hello);

                    byte[] buff = new byte[1024];
                    while (true)
                    {
                        int n = client.Receive(buff, 0, buff.Length, SocketFlags.None);

                        if (n == 0)
                        {
                            Console.WriteLine($"Client {client.RemoteEndPoint} đã ngắt kết nối.");
                            break;
                        }

                        string str = Encoding.ASCII.GetString(buff, 0, n);
                        Console.WriteLine($"[{client.RemoteEndPoint}] {str}");

                        if (str.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"Client {client.RemoteEndPoint} đã thoát");
                            client.Send(Encoding.ASCII.GetBytes("Bye"));
                            break;
                        }

                        string reply = HandleCalc(str);
                        client.Send(Encoding.ASCII.GetBytes(reply));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                }
                finally
                {
                    try { client?.Shutdown(SocketShutdown.Both); } catch { }
                    try { client?.Close(); } catch { }
                }
            }
        }

        static string HandleCalc(string input)
        {
            var parts = input.Split(' ');
            if (parts.Length != 3) return "Sai cú pháp. Dùng: a op b\n";

            if (!double.TryParse(parts[0], out double a) || !double.TryParse(parts[2], out double b))
                return "Sai toán hạng\n";

            string op = parts[1];
            switch (op)
            {
                case "+": return (a + b).ToString();
                case "-": return (a - b).ToString();
                case "*": return (a * b).ToString();
                case "/": return b == 0 ? "Lỗi: chia cho 0" : (a / b).ToString();
                case "%": return b == 0 ? "Lỗi: chia cho 0" : (a % b).ToString();
                case "^": return Math.Pow(a, b).ToString();
                default: return "Phép toán không hỗ trợ (+ - * / % ^)";
            }
        }
    }
}
