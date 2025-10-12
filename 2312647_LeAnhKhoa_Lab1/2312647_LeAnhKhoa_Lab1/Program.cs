using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _2312647_LeAnhKhoa_Lab1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			foreach(string arg in args)
			{
				Console.WriteLine("Phan giai ten mien: " + arg);
				GetHostInfo(arg);
            }
			Console.ReadKey();
		}

		static void GetHostInfo(string host)
		{
			try
			{
				IPHostEntry hostInfo = Dns.GetHostEntry(host);
				//Display host name
				Console.WriteLine("Ten mien: " + hostInfo.HostName);
				//Display list of IP address
				Console.Write("Dia chi IP: ");
				foreach (IPAddress ipaddr in hostInfo.AddressList)
					Console.Write(ipaddr.ToString() + " ");
				Console.WriteLine();
            }
			catch
			{
				Console.WriteLine("Khong phan giai duoc ten mien: " + host + "\n");
            }
		}
	}
}
