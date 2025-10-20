using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSocketServer
{
    public class AsyncSocketTCPServer
    {
        public event Action<string> OnMessageReceived;

        IPAddress mIP;
        int mPort;
        TcpListener mTcpListener;

        List<TcpClient> mClients;

        public bool KeepRunning {  get; set; }
        public AsyncSocketTCPServer()
        {
            mClients = new List<TcpClient>();
        }

        #region Phương thức lắng nghe, nhận kết nối từ Client, thêm vào danh sách
        public async void StartListeningForIncomingConnection(IPAddress ipaddr = null, int port = 9001)
        {
            if (ipaddr == null)
                ipaddr = IPAddress.Any;
            if (port <= 0)
                port = 9001;
            mIP = ipaddr;
            mPort = port;

            System.Diagnostics.Debug.WriteLine(string.Format($"IP Address: {mIP.ToString()} - Port: {mPort}"));

            mTcpListener = new TcpListener(mIP, mPort);

            try
            {
                mTcpListener.Start();

                KeepRunning = true;

                while (KeepRunning)
                {
                    var returnedByAccept = await mTcpListener.AcceptTcpClientAsync();

                    mClients.Add(returnedByAccept);

                    Debug.WriteLine(string.Format($"Client connected successfully, count {mClients.Count}, {returnedByAccept.Client.RemoteEndPoint}"));
                    TackCareOfTCPClient(returnedByAccept);
                }
            }
            catch(Exception excp)
            {
                System.Diagnostics.Debug.WriteLine(excp.ToString());
            }
        }
        #endregion

        #region Phương thức xóa client
        private void RemoveClient(TcpClient paramClient)
        {
            if (mClients.Contains(paramClient))
            {
                mClients.Remove(paramClient);
                Debug.WriteLine(String.Format($"Client removed, count: {mClients.Count}"));
            }
        }
        #endregion

        #region Phương thức quản lý các client, nhận tin nhắn từ client
        private async void TackCareOfTCPClient(TcpClient paramClient)
        {
            NetworkStream stream = null;
            StreamReader reader = null;

            try
            {
                stream = paramClient.GetStream();
                reader = new StreamReader(stream);

                char[] buff = new char[64];

                while (KeepRunning)
                {
                    Debug.WriteLine("*** Ready to read");

                    int nRet = await reader.ReadAsync(buff, 0, buff.Length);

                    System.Diagnostics.Debug.WriteLine("Returned: " + nRet);

                    if (nRet == 0)
                    {
                        RemoveClient(paramClient);

                        System.Diagnostics.Debug.WriteLine("Socket disvonnected");
                        break;
                    }
                    string receivedText = new string(buff);
                    OnMessageReceived?.Invoke(receivedText.Trim());

                    System.Diagnostics.Debug.WriteLine("*** RECEIVED: " + receivedText);

                    Array.Clear(buff, 0, buff.Length);
                }
            }
            catch (Exception excp)
            {
                RemoveClient(paramClient);
                System.Diagnostics.Debug.WriteLine(excp.ToString());
            }
        }
        #endregion

        #region Phương thức gửi tin nhắn đến tất cả các client
        public async void SendToAll(string leMessage)
        {
            if (string.IsNullOrEmpty(leMessage))
                return;

            try
            {
                byte[] buffMessage = Encoding.ASCII.GetBytes(leMessage + "\r\n");

                foreach (TcpClient c in mClients)
                    await c.GetStream().WriteAsync(buffMessage, 0, buffMessage.Length);
            }
            catch(Exception excp)
            {
                Debug.WriteLine(excp.ToString());
            }
        }
        #endregion

        #region Phương thức ngắt kết nối tất cả client và dừng server
        public void StopServer()
        {
            try
            {
                if (mTcpListener != null)
                    mTcpListener.Stop();
                foreach (TcpClient c in mClients)
                    c.Close();
                mClients.Clear();
            }
            catch (Exception excp)
            {
                Debug.WriteLine(excp.ToString());
            }
        }
        #endregion
    }
}