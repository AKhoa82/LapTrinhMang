using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.IO;

namespace Bai2_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Hướng dẫn
            //byte[] data = new byte[1024];
            //TcpListener server = new TcpListener(IPAddress.Any, 9050);
            //server.Start();
            //TcpClient client = server.AcceptTcpClient();
            //NetworkStream ns = client.GetStream();

            //byte[] size = new byte[2];
            //int recv = ns.Read(size, 0, 2);
            //int packsize = BitConverter.ToInt16(size, 0);
            //Console.WriteLine($"Kich thuoc goi tin = {packsize}");
            //recv = ns.Read(data, 0, packsize);
            //Employee emp1 = new Employee(data);
            //Console.WriteLine($"emp1.EmployeeID = {emp1.EmployeeID}");
            //Console.WriteLine($"emp1.LastName = {emp1.LastName}");
            //Console.WriteLine($"emp1.FirstName = {emp1.FirstName}");
            //Console.WriteLine($"emp1.YearsService = {emp1.YearsService}");
            //Console.WriteLine($"emp1.Salary = {emp1.Salary}\n");
            #endregion

            #region Yêu cầu nâng cao a) và b)
            byte[] data = new byte[1024];
            TcpListener server = new TcpListener(IPAddress.Any, 9050);
            server.Start();
            Console.WriteLine("Server dang cho ket noi...");

            TcpClient client = server.AcceptTcpClient();
            NetworkStream ns = client.GetStream();

            using (StreamWriter sw = new StreamWriter("EmployeeData.txt", true))
            {
                while (true)
                {
                    byte[] size = new byte[2];
                    int recv = ns.Read(size, 0, 2);
                    if (recv == 0)
                        break;

                    int packsize = BitConverter.ToInt16(size, 0);
                    int count = ns.Read(data, 0, packsize);

                    if (count == 0)
                        break;

                    Employee emp = new Employee(data);

                    Console.WriteLine("\n--- Thong tin nhan vien ---");
                    Console.WriteLine($"emp1.EmployeeID = {emp.EmployeeID}");
                    Console.WriteLine($"emp1.LastName = {emp.LastName}");
                    Console.WriteLine($"emp1.FirstName = {emp.FirstName}");
                    Console.WriteLine($"emp1.YearsService = {emp.YearsService}");
                    Console.WriteLine($"emp1.Salary = {emp.Salary}\n");

                    sw.WriteLine($"EmployeeID: {emp.EmployeeID}");
                    sw.WriteLine($"LastName: {emp.LastName}");
                    sw.WriteLine($"FirstName: {emp.FirstName}");
                    sw.WriteLine($"YearsService: {emp.YearsService}");
                    sw.WriteLine("------------------------------");
                    sw.Flush();
                }
            }
            #endregion

            ns.Close();
            client.Close();
            server.Stop();
            Console.WriteLine("\nDu lieu da duoc luu vao file EmployeeData.txt");
            Console.ReadLine();
        }
    }
}
