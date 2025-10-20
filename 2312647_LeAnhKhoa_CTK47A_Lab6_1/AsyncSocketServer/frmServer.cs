using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            txtText.Text = msg;
        }
    }
}
