using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsyncSocketTCP;

namespace AsyncSocketServer
{
    public partial class frmServer : Form
    {
        AsyncSocketTCPServer mServer;
        public frmServer()
        {
            InitializeComponent();
            mServer = new AsyncSocketTCPServer();
            mServer.OnMessageReceived += HienThiTinNhan;
            mServer.ClientConnectedEvent += HandleClientConnected;
            mServer.ClientDisconnectedEvent += HandleClientDisconnected;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            mServer.StartListeningForIncomingConnection();
        }

        private void btnSendAll_Click(object sender, EventArgs e)
        {
            mServer.SendToAll(txtText.Text.Trim());
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            mServer.StopServer();
        }

        private void frmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            mServer.StopServer();
        }

        private void HienThiTinNhan(string msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(HienThiTinNhan), msg);
                return;
            }
            txtMessage.Text = msg;
        }

        void HandleClientConnected(object sender, ClientConnectedEventArgs e)
        {
            txtClientInfo.AppendText(string.Format($"{DateTime.Now} - New client connected - {e.NewClient}{Environment.NewLine}"));
        }

        void HandleClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            txtClientInfo.AppendText(string.Format($"{DateTime.Now} - Client disconnected - {e.ClientInfo} | Remaining: {e.RemainingClients}{Environment.NewLine}"));
        }
    }
}