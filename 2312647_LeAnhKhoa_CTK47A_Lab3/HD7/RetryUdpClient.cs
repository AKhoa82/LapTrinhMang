using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HD7
{
    internal class RetryUdpClient
    {
        public int SndRcvData(Socket s, byte[] message, EndPoint rmtdevice)
        {
            int recv;
            int retry = 0;
            byte[] data;

            while (true)
            {
                Console.WriteLine($"Truyền lại lần thứ: #{retry}");
                try
                {
                    s.SendTo(message, message.Length, SocketFlags.None, rmtdevice);

                    data = new byte[1024];
                    recv = s.ReceiveFrom(data, ref rmtdevice);

                    if (recv > 0)
                        return recv;
                }
                catch (SocketException)
                {
                    recv = 0;
                }

                if (recv <= 0)
                {
                    retry++;
                    if (retry > 4)
                    {
                        Console.WriteLine("Không nhận được phản hồi sau 5 lần thử.");
                        return 0;
                    }
                }
            }
        }
    }
}
