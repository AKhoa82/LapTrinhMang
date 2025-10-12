using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Bai2
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Hướng dẫn
            //Employee emp1 = new Employee();

            //emp1.EmployeeID = 1;
            //emp1.LastName = "Nguyen";
            //emp1.FirstName = "Van A";
            //emp1.YearsService = 12;
            //emp1.Salary = 3500000;

            //TcpClient client;
            //try
            //{
            //    client = new TcpClient("127.0.0.1", 9050);
            //}
            //catch(SocketException)
            //{
            //    Console.WriteLine("Khong ket noi duoc voi server");
            //    return;
            //}
            //NetworkStream ns = client.GetStream();
            //byte[] data = emp1.GetBytes();
            //int size = emp1.size;
            //byte[] packsize = new byte[2];
            //Console.WriteLine($"Kich thuoc goi tin = {size}");
            //packsize = BitConverter.GetBytes(size);
            //ns.Write(packsize, 0, 2);
            //ns.Write(data, 0, size);
            //ns.Flush();
            #endregion

            #region Yêu cầu nâng cao a)
            TcpClient client;
            try
            {
                client = new TcpClient("127.0.0.1", 9050);
            }
            catch(SocketException)
            {
                Console.WriteLine("Khong ket noi duoc voi server");
                return;
            }

            NetworkStream ns = client.GetStream();

            while (true)
            {
                Employee emp = new Employee();

                Console.Write("Nhap EmployeeID: ");
                emp.EmployeeID = int.Parse(Console.ReadLine());

                Console.Write("Nhap LastName: ");
                emp.LastName = Console.ReadLine();

                Console.Write("Nhap FirstName: ");
                emp.FirstName = Console.ReadLine();

                Console.Write("Nhap so nam lam viec: ");
                emp.YearsService = int.Parse(Console.ReadLine());

                Console.Write("Nhap luong: ");
                emp.Salary = double.Parse(Console.ReadLine());

                byte[] data = emp.GetBytes();
                int size = emp.size;

                byte[] packsize = BitConverter.GetBytes(size);
                ns.Write(packsize, 0, 2);
                ns.Write(data, 0, size);
                ns.Flush();

                Console.Write("\nBan co muon tiep tuc khong (Khong de thoat)? ");
                string ans = Console.ReadLine();
                if (ans.Equals("Khong", StringComparison.OrdinalIgnoreCase))
                    break;
            }
            #endregion

            ns.Close();
            client.Close();
        }
    }
}
