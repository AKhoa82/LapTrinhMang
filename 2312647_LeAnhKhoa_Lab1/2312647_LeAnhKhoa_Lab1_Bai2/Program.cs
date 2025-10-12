using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _2312647_LeAnhKhoa_Lab1_Bai2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			ShowLocalHostNetworkInfo();
			Console.ReadKey();
		}
		static void ShowLocalHostNetworkInfo()
		{
			Console.WriteLine("Thong tin mang cua local host:\n");

			foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
			{
				// Bỏ qua interface không hoạt động
				if (ni.OperationalStatus != OperationalStatus.Up)
					continue;

				Console.WriteLine("Ten adapter: " + ni.Name);
				Console.WriteLine("Mo ta: " + ni.Description);
				Console.WriteLine("Loai: " + ni.NetworkInterfaceType);
				Console.WriteLine("Trang thai: " + ni.OperationalStatus);

				IPInterfaceProperties ipProps = ni.GetIPProperties();

				// Địa chỉ IP và Subnet Mask
				foreach (UnicastIPAddressInformation unicast in ipProps.UnicastAddresses)
				{
					if (unicast.Address.AddressFamily == AddressFamily.InterNetwork) // IPv4
					{
						Console.WriteLine("  Dia chi IP: " + unicast.Address);
						Console.WriteLine("  Subnet Mask: " + unicast.IPv4Mask);
					}
				}

				// Default Gateway
				foreach (GatewayIPAddressInformation gateway in ipProps.GatewayAddresses)
				{
					if (gateway.Address.AddressFamily == AddressFamily.InterNetwork)
					{
						Console.WriteLine("  Default Gateway: " + gateway.Address);
					}
				}
				Console.WriteLine(new string('-', 40));
			}
		}

	}
}
