using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bai3_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UdpClient server = new UdpClient(9050);
            IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            Console.WriteLine("Server UDP dang cho du lieu...");

            using (StreamWriter sw = new StreamWriter("EmployeeData_UDP.txt", true))
            {
                while (true)
                {
                    byte[] sizeBytes = server.Receive(ref remote);
                    int packsize = BitConverter.ToInt16(sizeBytes, 0);

                    byte[] data = server.Receive(ref remote);
                    if (data.Length == 0)
                        break;

                    Employee emp = new Employee(data);

                    Console.WriteLine("\n--- Thong tin nhan vien ---");
                    Console.WriteLine($"EmployeeID = {emp.EmployeeID}");
                    Console.WriteLine($"LastName = {emp.LastName}");
                    Console.WriteLine($"FirstName = {emp.FirstName}");
                    Console.WriteLine($"YearsService = {emp.YearsService}");
                    Console.WriteLine($"Salary = {emp.Salary}");

                    sw.WriteLine($"EmployeeID: {emp.EmployeeID}");
                    sw.WriteLine($"LastName: {emp.LastName}");
                    sw.WriteLine($"FirstName: {emp.FirstName}");
                    sw.WriteLine($"YearsService: {emp.YearsService}");
                    sw.WriteLine($"Salary: {emp.Salary}");
                    sw.WriteLine("----------------------------");
                    sw.Flush();
                } 
                    
            }
            server.Close();   
        }
    }
}
