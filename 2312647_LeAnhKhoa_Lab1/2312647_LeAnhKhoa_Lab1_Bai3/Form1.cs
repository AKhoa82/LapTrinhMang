using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2312647_LeAnhKhoa_Lab1_Bai3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblIPInfo.Text = "Thông tin giao thức IP: " + Dns.GetHostName();
        }

        private void btnResolve_Click(object sender, EventArgs e)
        {
            string host = txtHost.Text.Trim();
            lstResult.Items.Clear();

            if (string.IsNullOrEmpty(host))
            {
                MessageBox.Show("Vui lòng nhập tên miền!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(host);
                lstResult.Items.Add("Tên miền chính: " + hostInfo.HostName);
                lstResult.Items.Add("Địa chỉ IP:");

                foreach (IPAddress ip in hostInfo.AddressList)
                    lstResult.Items.Add(" - " + ip.ToString());
            }
            catch
            {
                lstResult.Items.Add("Không phân giải được tên miền: " + host);
            }
        }
    }
}
