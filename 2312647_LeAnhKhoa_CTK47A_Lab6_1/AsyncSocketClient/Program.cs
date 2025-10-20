using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AsyncSocketTCP;

namespace AsyncSocketClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            AsyncSocketTCPClient client = new AsyncSocketTCPClient();

            Console.WriteLine("*** Welcome to Async Socket Client ***");
            Console.Write("Enter Server IP Address (default = 127.0.0.1): ");
            string strIPAddress = Console.ReadLine();
            if (string.IsNullOrEmpty(strIPAddress))
                strIPAddress = "127.0.0.1";

            Console.Write("Enter Port Number (0 <= 65535) (default = 9001): ");
            string strPortInput = Console.ReadLine();
            if (string.IsNullOrEmpty(strPortInput))
                strPortInput = "9001";

            if (!client.SetServerIPAddress(strIPAddress) ||
                !client.SetPortNumber(strPortInput))
            {
                Console.WriteLine(
                    string.Format("IP Address or port number invalid- {0} - {1} - Press a key to exit",
                    strIPAddress, strPortInput));
                Console.ReadKey();
                return;
            }

            try
            {
                _ = client.ConnectToServer();
                await Task.Delay(500); //Chờ kết nối ổn định
            }
            catch (Exception excp)
            {
                Console.WriteLine("Loi ket noi den Server: " + excp.Message);
                Console.WriteLine("Chuong trinh se thoat...");
                Console.ReadKey();
                return;
            }
            string strInputUser = null;

            try
            {
                Console.WriteLine("Ban co the gui tin nhan. Go <exit> de thoat.");
                do
                {
                    strInputUser = Console.ReadLine();

                    if (strInputUser.Trim().ToLower() == "<exit>")
                    {
                        client.CloseAndDisconnect();
                    }
                    else if (!strInputUser.Equals("<exit>"))
                    {
                        await client.SendToServer(strInputUser);
                    }

                } while (strInputUser.Trim().ToLower() != "<exit>");
            }
            catch (Exception excp)
            {
                Console.WriteLine("Da xay ra loi trong qua trinh gui du lieu: " + excp.Message);
            }
            Console.WriteLine("Chuong trinh se thoat...");
            Console.ReadKey();
        }
    }
}
