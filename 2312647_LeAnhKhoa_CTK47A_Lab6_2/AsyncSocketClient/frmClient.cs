using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsyncSocketClient;
using AsyncSocketTCP;

namespace AsyncSocketClient
{
    public partial class frmClient : Form
    {
        private AsyncSocketTCPClient client;
        public frmClient()
        {
            InitializeComponent();
            client = new AsyncSocketTCPClient();
            client.OnMessageReceived += HienThiTinNhanTuServer;
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!client.SetServerIPAddress(txtIP.Text.Trim()) ||
                    !client.SetPortNumber(txtPort.Text.Trim()))
                {
                    MessageBox.Show("Sai IP hoặc Port!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _ = client.ConnectToServer();
                await Task.Delay(500);

                MessageBox.Show("Kết nối tới Server thành công!", "Thông báo");
            }
            catch (Exception excp)
            {
                MessageBox.Show("Không thể kết nối tới Server: " + excp.Message);
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtClientMessage.Text.Trim()))
                return;
            try
            {
                await client.SendToServer(txtClientMessage.Text);
                txtClientMessage.Clear();
                txtClientMessage.Focus();
            }
            catch (Exception excp)
            {
                MessageBox.Show("Lỗi khi gửi tin: " + excp.Message);
            }
        }

        private void HienThiTinNhanTuServer(string msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(HienThiTinNhanTuServer), msg);
                return;
            }

            txtServerMessage.AppendText($"{msg}{Environment.NewLine}");
        }

        private void frmClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                client.CloseAndDisconnect();
            }
            catch
            {

            }
        }
    }
}
