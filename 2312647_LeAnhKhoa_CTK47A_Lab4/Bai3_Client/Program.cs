using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bai3_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UdpClient client = new UdpClient();
            IPEndPoint remote = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            while (true) 
            {
                Employee emp = new Employee();

                Console.Write("Nhap EmployeeID: ");
                emp.EmployeeID = int.Parse(Console.ReadLine());

                Console.Write("Nhap LastName: ");
                emp.LastName = Console.ReadLine();

                Console.Write("Nhap FirstName: ");
                emp.FirstName = Console.ReadLine();

                Console.Write("Nhap So nam lam viec: ");
                emp.YearsService = int.Parse(Console.ReadLine());

                Console.Write("Nhap Luong: ");
                emp.Salary = double.Parse(Console.ReadLine());

                byte[] data = emp.GetBytes();
                int size = emp.size;

                byte[] packsize = BitConverter.GetBytes(size);

                client.Send(packsize, packsize.Length, remote);
                client.Send(data, size, remote);

                Console.Write("\nBan co muon tiep tuc khong (Khong de thoat)? ");
                string ans = Console.ReadLine();
                if (ans.Equals("Khong", StringComparison.OrdinalIgnoreCase))
                    break;
            }
            client.Close();
        }
    }
}
