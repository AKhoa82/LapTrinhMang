using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSocketTCP
{
    public class AsyncSocketTCPClient
    {
        public event Action<string> OnMessageReceived;

        IPAddress mServerIPAddress;
        int mServerPort;
        TcpClient mClient;

        public IPAddress ServerIPAddress
        {
            get
            {
                return mServerIPAddress;
            }
        }

        public int ServerPort
        {
            get
            {
                return mServerPort;
            }
        }

        public AsyncSocketTCPClient()
        {
            mClient = null;
            mServerPort = -1;
            mServerIPAddress = null;
        }

        #region Phương thức thiết đặt địa chỉ IP của Server
        public bool SetServerIPAddress(string _IPAddressServer)
        {
            IPAddress ipaddr = null;

            if (!IPAddress.TryParse(_IPAddressServer, out ipaddr))
            {
                Console.WriteLine("Invalid IP address.");
                return false;
            }

            mServerIPAddress = ipaddr;
            return true;
        }
        #endregion

        #region Phương thức thiết đặt Port number
        public bool SetPortNumber(string _ServerPort)
        {
            int portNumber = 0;

            if (!int.TryParse(_ServerPort.Trim(), out portNumber))
            {
                Console.WriteLine("Invalid port number.");
                return false;
            }

            if (portNumber <= 0 || portNumber > 65535)
            {
                Console.WriteLine("Port number must be betweent 0 to 65535.");
                return false;
            }

            mServerPort = portNumber;
            return true;
        }
        #endregion

        #region Phương thức đóng kết nối
        public void CloseAndDisconnect()
        {
            if (mClient != null)
                if (mClient.Connected)
                    mClient.Close();
        }
        #endregion

        #region Phương bất đồng bộ Kết nối đến Server
        public async Task ConnectToServer()
        {
            if (mClient == null)
                mClient = new TcpClient();

            try
            {
                await mClient.ConnectAsync(mServerIPAddress, mServerPort);
                Console.WriteLine(string.Format($"Connected to server IP/Port: {mServerIPAddress} / {mServerPort}"));

                await ReadDataAsync(mClient);
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
                throw;
            }
        }
        #endregion

        #region Phương bất đồng bộ Gửi dữ liệu đến Server
        public async Task SendToServer(string strInputUser)
        {
            if (string.IsNullOrEmpty(strInputUser))
            {
                Console.WriteLine("Empty message, no data sent.");
                return;
            }

            if (mClient != null)
            {
                if (mClient.Connected)
                {
                    StreamWriter clientStreamWriter = new StreamWriter(mClient.GetStream());
                    clientStreamWriter.AutoFlush = true;

                    await clientStreamWriter.WriteLineAsync(strInputUser);
                    Console.WriteLine("Data sent...");
                }
            }
        }
        #endregion

        #region Phương bất đồng bộ Nhận dữ liệu từ Server
        private async Task ReadDataAsync(TcpClient mClient)
        {
            try
            {
                StreamReader clientStreamReader = new StreamReader(mClient.GetStream());
                char[] buff = new char[64];
                int readByteCount = 0;

                while (true)
                {
                    readByteCount = await clientStreamReader.ReadAsync(buff, 0, buff.Length);

                    if (readByteCount <= 0)
                    {
                        Console.WriteLine("Disconnected from server.");
                        mClient.Close();
                        break;
                    }
                    string receivedText = new string(buff).Trim();
                    Console.WriteLine($"Received bytes: {receivedText}");

                    OnMessageReceived?.Invoke(receivedText);
                    Array.Clear(buff, 0, buff.Length);
                }
            }
            catch (ObjectDisposedException)
            {

            }
            catch (Exception excp)
            {
                Console.WriteLine("Error in receiving: " + excp.Message);
            }
        }
        #endregion
    }
}